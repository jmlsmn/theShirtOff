using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Services;
using DomainModel.Abstract.Entities;
using DomainModel.Abstract.Repositories;
using DomainModel.Concrete.DataEntities;
using DomainModel.Concrete.Extensions;
using DomainModel.Concrete.Helpers;
using DomainModel.Concrete.Exceptions;
using DomainModel.Concrete.Enums;
using DomainModel.Concrete.Helpers.Email;
using DomainModel.Concrete.DataObjects;

namespace DomainModel.Concrete.Services
{
    class AccountService : IAccountService
    {
        #region Private Members

        private readonly IAccountRepository _accountRepository;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IImageRepository _imageRepository;
        private readonly int _activationExpiration = 2;

        #endregion

        #region Constructors

        public AccountService(IAccountRepository accountRepository, 
                              ISubmissionRepository submissionRepository, 
                              IImageRepository imageRepository)
        {
            _accountRepository = accountRepository;
            _submissionRepository = submissionRepository;
            _imageRepository = imageRepository;
        }

        #endregion

        #region IAccountService Methods
        public void Vote(IAccount account, int submissionID)
        {
            if (account.AccountID == 0)
                account = GetOrCreateTokenAccount(account);

            IVote vote = new Vote(){ AccountID = account.AccountID,
                                     SubmissionID = submissionID,
                                     VoteDate = DateTime.Now};

            _submissionRepository.AddVote(vote);
        }

        public void AddSubmission(ISubmission submission)
        {
            if (_submissionRepository.GetContestSubmissionByAccountID(submission.ContestID, submission.AccountID) != null)
                throw new DuplicateSubmissionException();

            var sub = submission.ConvertTo<ISubmission>(typeof(Submission));
            _submissionRepository.AddSubmission(sub);
            foreach (IImage img in sub.SubmissionImages)
            {
                var newImage = img.ConvertTo<IImage>(typeof(Image));
                _imageRepository.AddImage(newImage);
                _submissionRepository.AddSubmissionImage(sub.SubmissionID, newImage.ImageID);
            }
        }

        public IAccount AddAccount(IAccount account)
        {
            account.Password = EncryptAccountPassword(account.Password);
            account.IsActive = false;
            account = CreateAccount(account);
            SendEmailActivation(account.AccountID, account.Email);
            return account;
        }

        public IAccount AuthenticateAccount(IAccount account)
        {
            if (string.IsNullOrEmpty(account.Token))
            {
                return AuthenticateByEmail(account.Email, account.Password);
            }
            else
            {
                return AuthenticateByToken(account);
            }
        }

        public bool ActivateAccount(int accountID, Guid guid)
        {
            IEmailActivation activation = _accountRepository.GetEmailActivation(accountID);
            if (activation.GUID.Equals(guid) && activation.CreationDate.AddDays(_activationExpiration) >= DateTime.Now)
            {
                IAccount accountToActivate = _accountRepository.GetAccount(accountID);
                accountToActivate.IsActive = true;
                _accountRepository.UpdateAccount(accountToActivate);
                return true;
            }
            return false;
        }

        public void ChangeEmail(int accountID, string newEmail)
        {
            SendChangeEmail(accountID, newEmail);
        }

        #endregion

        #region Private Methods

        private void SendEmailActivation(int accountID, string email)
        {
            IEmailActivation newActivation = new EmailActivation();

            newActivation.AccountID =  accountID;
            newActivation.GUID = Guid.NewGuid();
            newActivation.CreationDate = DateTime.Now;

            _accountRepository.AddEmailActivation(newActivation);
            EmailData data = new EmailData();
            data.To = email;
            //TODO:get email activation subject and body, create link and guid as querystring param &guid=
            Email.Send(data);
        }

        private void SendChangeEmail(int accountID, string newEmail)
        {
            IEmailActivation newActivation = new EmailActivation();

            newActivation.AccountID = accountID;
            newActivation.GUID = Guid.NewGuid();
            newActivation.CreationDate = DateTime.Now;

            _accountRepository.AddEmailActivation(newActivation);
            EmailData data = new EmailData();
            data.To = newEmail;
            //TODO:get email activation subject and body, create link and add email and guid as querystring params &email=, &guid=
            Email.Send(data);
        }

        private IAccount GetOrCreateTokenAccount(IAccount account)
        {
            IAccount newAccount = null;
            if (!string.IsNullOrEmpty(account.Token))
            {
                newAccount = _accountRepository.GetAccountByToken(account.Token);

                if (newAccount == null)
                {
                    newAccount = CreateAccountWithToken(account);
                }
            }
            return newAccount;
        }

        private IAccount CreateAccount(IAccount account)
        {
            account.Balance = 0;
            account.CreationDate = DateTime.Now;
            account.Rank = UserRank.SimpleTailor;

            var acc = account.ConvertTo<IAccount>(typeof(Account));
            _accountRepository.AddAccount(acc);
            return acc;
        }

        private string EncryptAccountPassword(string password)
        {
            string salt = Encryption.GenerateSalt();
            string hashPassword = Encryption.Encrypt(password, salt);

            return string.Join(";", hashPassword, salt);
        }

        private IAccount AuthenticateByEmail(string email, string password)
        {
            string[] storedValues;
            string salt = string.Empty;
            string hashPassword = string.Empty;

            var account = _accountRepository.GetAccountByEmail(email);

            if (account != null)
            {
                storedValues = account.Password.Split(';');
                hashPassword = storedValues[0];
                salt = storedValues[1];

                if (Encryption.Verify(password, hashPassword, salt))
                    return account;
            }

            return null;
        }

        private IAccount AuthenticateByToken(IAccount account)
        {
            return GetOrCreateTokenAccount(account);
        }

        private IAccount CreateAccountWithToken(IAccount account)
        {
            account.IsActive = true;
            account = CreateAccount(account);
            return account;
        }

        #endregion        
        
    }
}

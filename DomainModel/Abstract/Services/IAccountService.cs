using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Services
{
    public interface IAccountService
    {
        void Vote(IAccount account, int submissionID);
        void AddSubmission(ISubmission submission);
        IAccount AuthenticateAccount(IAccount account);
        IAccount AddAccount(IAccount account);
        bool ActivateAccount(int accountID, Guid guid);
        void ChangeEmail(int accountID, string newEmail);
    }
}

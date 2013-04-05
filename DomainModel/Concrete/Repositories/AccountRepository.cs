using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Repositories;
using DomainModel.Abstract.Entities;
using PetaPoco;
using DomainModel.Concrete.DataEntities;
using DomainModel.Concrete.DataObjects;

namespace DomainModel.Concrete.Repositories
{
    class AccountRepository : IAccountRepository
    {
        #region Private Members

        private string _connectionStringName;
        private Database _db;

        #endregion

        #region Constructors

        public AccountRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            InitializeDatabase();
        }

        #endregion

        #region Private Methods

        private void InitializeDatabase()
        {
            _db = new Database(_connectionStringName);
            _db.EnableAutoSelect = false;
            _db.EnableNamedParams = false;
            _db.ForceDateTimesToUtc = false;
        }

        #endregion

        #region IAccountRepository Methods

        public void AddAccount(IAccount account)
        {
           _db.Insert((Account)account);
        }

        public void UpdateAccount(IAccount account)
        {
            _db.Update(account);
        }

        public IAccount GetAccountByToken(string token)
        {
            var account = _db.SingleOrDefault<Account>("SELECT * FROM Accounts WHERE Token = @0", token);
            return account;
        }

        public IAccount GetAccountByEmail(string email)
        {
            var account = _db.SingleOrDefault<Account>("SELECT * FROM Accounts WHERE Email = @0", email);
            return account;
        }

        public IAccount GetAccount(int accountID)
        {
           var account = _db.SingleOrDefault<Account>("SELECT * FROM Accounts WHERE AccountID = @0", accountID);
           return account;
        }

        public void AddEmailActivation(IEmailActivation activation)
        {
            _db.Insert((EmailActivation)activation);
        }

        public IEmailActivation GetEmailActivation(int accountID)
        {
            var emailActivation = _db.SingleOrDefault<EmailActivation>("SELECT * FROM EmailActivations WHERE AccountID = @0", accountID);
            return emailActivation;
        }

        #endregion
    }
}

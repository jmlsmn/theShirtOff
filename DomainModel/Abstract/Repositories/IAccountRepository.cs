using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Repositories
{
    public interface IAccountRepository
    {
        void AddAccount(IAccount account);
        void AddEmailActivation(IEmailActivation activation);
        void UpdateAccount(IAccount account);
        IAccount GetAccount(int accountID);
        IAccount GetAccountByToken(string token);
        IAccount GetAccountByEmail(string email);
        IEmailActivation GetEmailActivation(int accountID);
    }
}

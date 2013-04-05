using DomainModel.Concrete.Enums;
using System;
namespace DomainModel.Abstract.Entities
{
    public interface IAccount
    {
        int AccountID { get; set; }
        string DisplayName { get; set; }
        string Token { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        decimal Balance { get; set; }
        UserRank Rank { get; set; }
        bool IsActive { get; set; }
        DateTime CreationDate { get; set; }
    }
}

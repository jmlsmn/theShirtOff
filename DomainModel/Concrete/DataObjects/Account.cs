using System;
using DomainModel.Abstract.Entities;
using PetaPoco;
using DomainModel.Concrete.Enums;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Accounts")]
    [PrimaryKey("AccountID")]
    internal class Account : IAccount
    {
        public int AccountID { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        [Column("UserRankID")]
        public UserRank Rank { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public string DisplayName { get; set; }
    }
}

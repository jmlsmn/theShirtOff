using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Abstract.Entities;
using DomainModel.Concrete.Enums;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text;
using Contest.Generic.Helpers.Attributes;

namespace Contest.Generic.Models
{
    public class AccountModel : IAccount
    {
        public AccountModel(string delimitedAccountString)
        {
            string[] accountObject = delimitedAccountString.Split(new string[]{"||"}, StringSplitOptions.None);
            if (accountObject.Length == 4)
            {
                AccountID = Convert.ToInt32(accountObject[0]);
                Token = accountObject[1];
                Email = accountObject[2];
                Password = accountObject[3];
            }
        }

        public AccountModel()
        {
        }

        public int AccountID { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public string Token { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public decimal Balance { get; set; }

        public UserRank Rank { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
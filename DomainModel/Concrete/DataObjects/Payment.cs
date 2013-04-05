using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using DomainModel.Concrete.Enums;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Payments")]
    [PrimaryKey("PaymentID")]
    internal class Payment : IPayment
    {
        public int PaymentID { get; set; }

        public decimal PaymentAmount { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}

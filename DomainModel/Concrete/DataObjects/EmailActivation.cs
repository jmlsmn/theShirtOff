using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataObjects
{
    [TableName("EmailActivations")]
    public class EmailActivation : IEmailActivation
    {
        public int AccountID { get; set; }

        public Guid GUID { get; set; }

        public DateTime CreationDate { get; set; }
    }
}

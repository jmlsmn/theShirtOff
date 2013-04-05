using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Abstract.Entities
{
    public interface IEmailActivation
    {
        int AccountID { get; set; }
        Guid GUID {get; set;}
        DateTime CreationDate {get; set;}
    }
}

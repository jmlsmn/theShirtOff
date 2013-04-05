using System;
using System.Collections.Generic;
using DomainModel.Concrete.Enums;

namespace DomainModel.Abstract.Entities
{
    public interface IOrder
    {
        int OrderID { get; set; }
        int AccountID { get; set; }
        OrderStatus OrderStatus { get; set; }
        DateTime OrderDate { get; set; }
        IEnumerable<IOrderItem> OrderItems { get; set; }
        decimal OrderTotal { get; set; }
    }
}

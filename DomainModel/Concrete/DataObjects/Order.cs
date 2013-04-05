using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Concrete.Enums;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Order")]
    [PrimaryKey("OrderID")]
    internal class Order : IOrder
    {
        public int OrderID { get; set; }

        public int AccountID { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public IEnumerable<IOrderItem> OrderItems { get; set; }

        public decimal OrderTotal { get; set; }
    }
}

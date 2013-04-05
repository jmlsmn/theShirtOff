using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("OrderItems")]
    [PrimaryKey("OrderItemID")]
    internal class OrderItem : IOrderItem
    {
        public int OrderItemID { get; set; }

        public IProduct OrderProduct { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<string> OrderItemAttributes { get; set; }

        public decimal OrderItemTotal { get; set; }
    }
}

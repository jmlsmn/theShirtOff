using System.Collections.Generic;

namespace DomainModel.Abstract.Entities
{
    public interface IOrderItem
    {
        int OrderItemID { get; set; }
        IProduct OrderProduct { get; set; }
        int Quantity { get; set; }
        IEnumerable<string> OrderItemAttributes { get; set; }
        decimal OrderItemTotal { get; set; }
    }
}

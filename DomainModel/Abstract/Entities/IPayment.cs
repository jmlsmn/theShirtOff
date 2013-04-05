using DomainModel.Concrete.Enums;

namespace DomainModel.Abstract.Entities
{
    public interface IPayment
    {
        int PaymentID { get; set; }
        decimal PaymentAmount { get; set; }
        PaymentType PaymentType { get; set; }
    }
}

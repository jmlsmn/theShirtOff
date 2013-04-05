using DomainModel.Concrete.DataEntities;
namespace DomainModel.Abstract.Entities
{
    public interface IProductAttribute
    {
        string Description { get; set; }
        IProductAttributeCategory Category { get; set; }
    }
}

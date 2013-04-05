using System.Collections.Generic;
using DomainModel.Concrete.Enums;
using System;

namespace DomainModel.Abstract.Entities
{
    public interface IProduct
    {
        int ProductID { get; set; }

        ProductType ProductType { get; set; }

        string Name { get; set; }

        string Description { get; set; }

        IEnumerable<IImage> ProductImages { get; set; }

        decimal Price { get; set; }

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        IEnumerable<IProductAttribute> ProductAttributes { get; set; }

        int AccountID { get; set; }
    }
}

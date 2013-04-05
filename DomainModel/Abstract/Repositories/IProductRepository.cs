using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Repositories
{
    public interface IProductRepository
    {
        IQueryable<IProduct> Products { get; }
        void AddProduct(IProduct product);
        void UpdateProduct(IProduct product);
        void DeleteProduct(int productID);
        void AddProductAttribute(int productID, string productAttribute);
    }
}

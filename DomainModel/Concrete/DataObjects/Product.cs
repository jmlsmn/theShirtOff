using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using DomainModel.Concrete.Enums;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Products")]
    [PrimaryKey("ProductID")]
    internal class Product : IProduct
    {
        public int ProductID { get; set; }

        public ProductType ProductType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<IImage> ProductImages { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<IProductAttribute> ProductAttributes { get; set; }

        public int AccountID { get; set; }
    }
}

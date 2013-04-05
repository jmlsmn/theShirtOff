using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("ProductAttribute")]
    internal class ProductAttribute : IProductAttribute
    {
        public string Description { get; set; }

        public IProductAttributeCategory Category { get; set; }
    }
}

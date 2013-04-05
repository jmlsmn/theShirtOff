using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("ProductAttributeCategories")]
    [PrimaryKey("ProductAttributeCategoryID")]
    internal class ProductAttributeCategory : IProductAttributeCategory
    {
        public int ProductAttributeCategoryID { get; set; }

        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Themes")]
    [PrimaryKey("ThemeID")]
    internal class Theme : ITheme
    {
        public int ThemeID { get; set; }

        public string Description { get; set; }
    }
}

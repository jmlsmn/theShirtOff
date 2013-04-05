using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Contests")]
    [PrimaryKey("ContestID")]
    internal class Contest : IContest
    {
        public int ContestID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ITheme Theme { get; set; }

        public bool IsActive { get; set; }

    }
}

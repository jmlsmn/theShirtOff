using System;
using System.Collections.Generic;

namespace DomainModel.Abstract.Entities
{
    public interface IContest
    {
        int ContestID { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        ITheme Theme { get; set; }
        bool IsActive { get; set; }
    }
}

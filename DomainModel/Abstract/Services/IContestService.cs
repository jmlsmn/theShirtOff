using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Services
{
    public interface IContestService
    {
       IContest GetCurrentContest();
       IEnumerable<ISubmission> GetLatestSubmissions(int contestID);
       IEnumerable<ISubmission> GetFeaturedSubmissions(int contestID);
    }
}

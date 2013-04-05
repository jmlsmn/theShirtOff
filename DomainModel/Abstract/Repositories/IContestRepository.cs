using System.Collections.Generic;
using System.Linq;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Repositories
{
    public interface IContestRepository
    {
        IEnumerable<IContest> Contests { get; }
        IEnumerable<ISubmission> GetAllContestSubmissions(int contestID);
        IEnumerable<ISubmission> GetLatestSubmissions(int contestID);
        IEnumerable<ISubmission> GetFeaturedSubmissions(int contestID);
        IEnumerable<ISubmission> GetUnapprovedSubmissions(int contestID);
        void AddContest(IContest contest);
        void UpdateContest(IContest contest);
        void DeleteContest(int contestID);
        IContest GetCurrentContest();
    }
}

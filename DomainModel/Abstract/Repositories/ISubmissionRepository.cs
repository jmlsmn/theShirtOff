using System.Linq;
using DomainModel.Abstract.Entities;
using System.Collections.Generic;
using DomainModel.Concrete.DataEntities;

namespace DomainModel.Abstract.Repositories
{
    public interface ISubmissionRepository
    {
        void AddSubmission(ISubmission submission);
        void AddSubmissionImage(int submissionID, int imageID);
        void AddVote(IVote vote);
        void UpdateSubmission(ISubmission submission);
        int GetVoteCount(int submissionID);
        void DeleteSubmission(int submissionID);
        IEnumerable<IImage> GetSubmissionImages(int submissionID);
        IImage GetSubmissionImage(int imageID);
        ISubmission GetSubmission(int submissionID);
        ISubmission GetContestSubmissionByAccountID(int contestID, int accountID);
    }
}

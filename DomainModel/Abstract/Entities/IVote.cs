using System;

namespace DomainModel.Abstract.Entities
{
    public interface IVote
    {
        int AccountID { get; set; }
        int SubmissionID { get; set; }
        DateTime VoteDate { get; set; }
    }
}

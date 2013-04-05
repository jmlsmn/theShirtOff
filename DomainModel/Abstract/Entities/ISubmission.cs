using System;
using System.Collections.Generic;
using DomainModel.Concrete.DataEntities;
using DomainModel.Concrete.Enums;

namespace DomainModel.Abstract.Entities
{
    public interface ISubmission
    {
        int SubmissionID { get; set; }
        int ContestID { get; set; }
        int AccountID { get; set; }
        SubmissionType SubmissionType { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string DisplayName { get; set; }
        bool IsApproved { get; set; }
        int VoteCount { get; set; }
        IEnumerable<IImage> SubmissionImages { get; set; }
        DateTime SubmissionDate { get; set; }
        DateTime? ApprovalDate { get; set; }
    }
}


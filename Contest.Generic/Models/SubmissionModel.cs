using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Abstract.Entities;
using DomainModel.Concrete.Enums;

namespace Contest.Generic.Models
{
    public class SubmissionModel : ISubmission
    {
        public int SubmissionID { get; set; }

        public int AccountID { get; set; }

        public string DisplayName { get; set; }

        public int ContestID { get; set; }

        public SubmissionType SubmissionType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsApproved { get; set; }

        public int VoteCount { get; set; }

        public IEnumerable<IImage> SubmissionImages { get; set; }

        public DateTime SubmissionDate { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}
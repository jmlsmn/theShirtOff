using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using DomainModel.Concrete.Enums;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Submissions")]
    [PrimaryKey("SubmissionID")]
    internal class Submission : ISubmission
    {
        public int SubmissionID { get; set; }

        public int ContestID { get; set; }

        public int AccountID { get; set; }

        [Column("SubmissionTypeID")]
        public SubmissionType SubmissionType { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Ignore]
        public string DisplayName { get; set; }

        public bool IsApproved { get; set; }

        [ResultColumn]
        public int VoteCount { get; set; }

        [Ignore]
        public IEnumerable<IImage> SubmissionImages { get; set; }

        public DateTime SubmissionDate { get; set; }

        public DateTime? ApprovalDate { get; set; }
    }
}

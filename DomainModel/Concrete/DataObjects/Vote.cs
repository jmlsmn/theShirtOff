using DomainModel.Abstract.Entities;
using PetaPoco;
using System;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Votes")]
    internal class Vote : IVote
    {
        public int AccountID { get; set; }

        public int SubmissionID { get; set; }

        public DateTime VoteDate { get; set; }
    }
}

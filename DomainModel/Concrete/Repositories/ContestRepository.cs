using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Repositories;
using DomainModel.Abstract.Entities;
using PetaPoco;
using DomainModel.Concrete.DataEntities;

namespace DomainModel.Concrete.Repositories
{
    class ContestRepository : IContestRepository
    {
        #region Private Members

        private string _connectionStringName;
        private Database _db;

        #endregion

        #region Constructor
       
        public ContestRepository(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
            InitializeDatabase();
        }

        #endregion

        #region Private Methods

        private void InitializeDatabase()
        {
            _db = new Database(_connectionStringName);
            _db.EnableAutoSelect = false;
            _db.EnableNamedParams = false;
            _db.ForceDateTimesToUtc = false;
        }

        #endregion

        #region IContestRepository Methods

        public IEnumerable<IContest> Contests
        {
            get 
            {
                var contests = _db.Fetch<Contest>("SELECT * FROM Contests");
                return contests;
            }
        }

        public IEnumerable<ISubmission> GetAllContestSubmissions(int contestID)
        {
            var submissions = _db.Query<Submission>("SELECT * FROM Submissions WHERE ContestID = @0",contestID);
            return submissions;
        }

        public IEnumerable<ISubmission> GetLatestSubmissions(int contestID)
        {
            //TODO: check for isApproved flag
            var sql = Sql.Builder
                     .Append("SELECT TOP 10 subs.*,")
                     .Append("Accounts.DisplayName")
                     .Append("FROM	Submissions AS subs")
                     .Append("JOIN Accounts ON Accounts.AccountID = subs.AccountID")
                     .Append("ORDER BY subs.ApprovalDate");

            var submissions = _db.Fetch<Submission>(sql);
            return submissions;
        }

        /*
         Query for easy reading:
         **************************************************************
            SELECT TOP 10  Submissions.SubmissionID,
	                       Submissions.SubmissionTypeID,
	                       Submissions.ContestID,
	                       Submissions.[Description],
	                       Submissions.IsApproved,
	                       Submissions.ApprovalDate,
	                       Submissions.SubmissionDate,
	                       COUNT(Votes.SubmissionID) AS VoteCount
            FROM Submissions 
            LEFT JOIN Votes ON Votes.SubmissionID = Submissions.SubmissionID
            WHERE Submissions.ContestID = @0
            GROUP BY Submissions.SubmissionID,
		             Submissions.SubmissionTypeID,
		             Submissions.ContestID,
		             Submissions.[Description],
		             Submissions.IsApproved,
		             Submissions.ApprovalDate,
		             Submissions.SubmissionDate
            ORDER BY VoteCount DESC
         **************************************************************
         */
        public IEnumerable<ISubmission> GetFeaturedSubmissions(int contestID)
        {
            var sql = Sql.Builder
                      .Append("SELECT  subs.*,")
                      .Append("Accounts.DisplayName")
                      .Append("FROM	Submissions AS subs")
                      .Append("JOIN (")
                      .Append("SELECT TOP 3 subs.SubmissionID, ")
                      .Append("COUNT(*) AS VoteCount")
                      .Append("FROM Submissions AS subs")
                      .Append("JOIN Votes AS vts")
                      .Append("ON subs.SubmissionID = vts.SubmissionID")
                      .Append("WHERE subs.ContestID = @0", contestID)
                      .Append("AND subs.IsApproved = @0", 1)
                      .Append("GROUP BY subs.SubmissionID")
                      .Append("ORDER BY COUNT(*) DESC, subs.SubmissionID")
                      .Append(") AS subvotecount")
                      .Append("ON subvotecount.SubmissionID = subs.SubmissionID")
                      .Append("JOIN Accounts ON Accounts.AccountID = subs.AccountID");


            var submissions = _db.Fetch<Submission>(sql);
            return submissions;
        }

        public IEnumerable<ISubmission> GetUnapprovedSubmissions(int contestID)
        {
            var submissions = _db.Fetch<Submission>("SELECT * FROM Submissions WHERE ContestID = @0 AND IsApproved = @1",
                                                    contestID, 0);
            return submissions;
        }

        public void AddContest(IContest contest)
        {
            _db.Insert(contest);
        }

        public void UpdateContest(IContest contest)
        {
            _db.Update(contest);
        }

        public void DeleteContest(int contestID)
        {
            _db.Delete<Contest>(contestID);
        }

        public IContest GetCurrentContest()
        {
            var contest = _db.SingleOrDefault<Contest>("SELECT * FROM Contests WHERE StartDate <= @0 AND EndDate > @0 AND IsActive = @1", DateTime.Now, 1);
            return contest;
        }

        #endregion        
    }
}

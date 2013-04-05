using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Repositories;
using PetaPoco;
using DomainModel.Abstract.Entities;
using DomainModel.Concrete.DataEntities;

namespace DomainModel.Concrete.Repositories
{
    class SubmissionRepository : ISubmissionRepository
    {
        #region Private Members
        
        private string _connectionStringName;
        private Database _db;

        #endregion

        #region Constructors
        public SubmissionRepository(string connectionStringName)
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

        #region ISubmissionRepository Methods

        public void AddSubmission(ISubmission submission)
        {
            _db.Insert(submission);
        }

        public void AddSubmissionImage(int submissionID, int imageID)
        {
            _db.Execute("INSERT INTO SubmissionImages (SubmissionID, ImageID) VALUES (@0, @1)",submissionID, imageID);
        }

        public void AddVote(IVote vote)
        {
            _db.Insert(vote);
        }

        public void UpdateSubmission(ISubmission submission)
        {
            _db.Update(submission);
        }

        public int GetVoteCount(int submissionID)
        {
            int result = _db.ExecuteScalar<int>("SELECT COUNT(*) FROM Votes WHERE SubmissionID = @0", submissionID);
            return result;
        }

        public void DeleteSubmission(int submissionID)
        {
            _db.Delete<Submission>(submissionID);
        }

        public IEnumerable<IImage> GetSubmissionImages(int submissionID)
        {
            var sql = Sql.Builder
                      .Append("SELECT imgs.* FROM SubmissionImages AS subImgs")
                      .Append("JOIN Images AS imgs")
                      .Append("ON subImgs.ImageID = imgs.ImageID")
                      .Append("WHERE subImgs.SubmissionID = @0",submissionID);
            var images = _db.Fetch<Image>(sql);
            return images;
        }

        public IImage GetSubmissionImage(int imageID)
        {
            var image = _db.SingleOrDefault<Image>("SELECT * FROM Images WHERE ImageID = @0",imageID);
            return image; 
        }

        public ISubmission GetSubmission(int submissionID)
        {       
            var sql = Sql.Builder
                         .Append("SELECT Submissions.SubmissionID,")
                         .Append("Submissions.AccountID,")
                         .Append("Submissions.SubmissionTypeID AS SubmissionType,")
                         .Append("Submissions.ContestID,")
                         .Append("Submissions.[Description],")
                         .Append("Submissions.IsApproved,")
                         .Append("Submissions.ApprovalDate,")
                         .Append("Submissions.SubmissionDate,")
                         .Append("COUNT(Votes.SubmissionID) AS VoteCount,")
                         .Append("Accounts.DisplayName")
                         .Append("FROM Submissions")
                         .Append("LEFT JOIN Votes ON Votes.SubmissionID = Submissions.SubmissionID")
                         .Append("LEFT JOIN Accounts ON Accounts.AccountID = Submissions.AccountID")
                         .Append("WHERE Submissions.SubmissionID = @0", submissionID)
                         .Append("GROUP BY Submissions.SubmissionID,")
                         .Append("Submissions.AccountID,")
                         .Append("Submissions.SubmissionTypeID,")
                         .Append("Submissions.ContestID,")
                         .Append("Submissions.[Description],")
                         .Append("Submissions.IsApproved,")
                         .Append("Submissions.ApprovalDate,")
                         .Append("Submissions.SubmissionDate,")
                         .Append("Accounts.DisplayName");

            var submission = _db.SingleOrDefault<Submission>(sql);
            return submission;
        }

        public ISubmission GetContestSubmissionByAccountID(int contestID, int accountID)
        {
            var submission = _db.SingleOrDefault<Submission>("SELECT * FROM Submissions WHERE ContestID = @0 AND AccountID = @1", 
                                                                                                                       contestID, 
                                                                                                                       accountID);
            return submission;
        }

        #endregion
    }
}

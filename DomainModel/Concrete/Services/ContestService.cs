using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Services;
using DomainModel.Abstract.Entities;
using DomainModel.Abstract.Repositories;

namespace DomainModel.Concrete.Services
{
    class ContestService : IContestService
    {
        #region Private Members

        private readonly IContestRepository _contestRepository;
        private readonly ISubmissionRepository _submissionRepository;

        #endregion

        #region Constructors

        public ContestService(IContestRepository contestRepository, ISubmissionRepository submissionRepository)
        {
            _contestRepository = contestRepository;
            _submissionRepository = submissionRepository;
        }

        #endregion

        #region IContestService Methods

        public IContest GetCurrentContest()
        {
           return _contestRepository.GetCurrentContest();
        }

        public IEnumerable<ISubmission> GetLatestSubmissions(int contestID)
        {
            IEnumerable<ISubmission> latestSubmissions = _contestRepository.GetLatestSubmissions(contestID);

            foreach (var submission in latestSubmissions)
            {
                submission.SubmissionImages = _submissionRepository.GetSubmissionImages(submission.SubmissionID);
            }

            return latestSubmissions;
        }

        public IEnumerable<ISubmission> GetFeaturedSubmissions(int contestID)
        {
            IEnumerable<ISubmission> featuredSubmissions = _contestRepository.GetFeaturedSubmissions(contestID);

            foreach (var submission in featuredSubmissions)
            {
                submission.SubmissionImages = _submissionRepository.GetSubmissionImages(submission.SubmissionID);
            }

            return featuredSubmissions;
        }

        #endregion
    }
}

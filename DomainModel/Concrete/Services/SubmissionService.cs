using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Services;
using DomainModel.Abstract.Repositories;
using DomainModel.Abstract.Entities;

namespace DomainModel.Concrete.Services
{
    class SubmissionService: ISubmissionService
    {
        #region Private Members
        
        private readonly ISubmissionRepository _submissionRepository;

        #endregion

        #region Constructors

        public SubmissionService(ISubmissionRepository submissionRepository)
        {
            _submissionRepository = submissionRepository;
        }

        #endregion

        #region ISubmissionService Methods

        public int GetVoteCount(int submissionID)
        {
            return _submissionRepository.GetVoteCount(submissionID);
        }

        public ISubmission GetSubmission(int submissionID)
        {
            ISubmission submission = _submissionRepository.GetSubmission(submissionID);
            submission.SubmissionImages = GetSubmissionImages(submission.SubmissionID);
            return submission;
        }

        public IEnumerable<IImage> GetSubmissionImages(int submissionID)
        {
            return _submissionRepository.GetSubmissionImages(submissionID);
        }

        public IImage GetSubmissionImage(int imageID)
        {
            return _submissionRepository.GetSubmissionImage(imageID);
        }
        #endregion
       
    }
}

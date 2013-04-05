using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Services
{
    public interface ISubmissionService
    {
        int GetVoteCount(int submissionID);
        ISubmission GetSubmission(int submissionID);
        IEnumerable<IImage> GetSubmissionImages(int submissionID);
        IImage GetSubmissionImage(int imageID);
    }
}

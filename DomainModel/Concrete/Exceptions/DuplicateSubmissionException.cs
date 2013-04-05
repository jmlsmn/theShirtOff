using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Concrete.Exceptions
{
    class DuplicateSubmissionException : SystemException
    {
        public DuplicateSubmissionException()
        {
        }

        public DuplicateSubmissionException(string message)
            : base(message)
        {
        }
    }
}

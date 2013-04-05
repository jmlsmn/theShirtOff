using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;

namespace DomainModel.Abstract.Repositories
{
    interface IImageRepository
    {
        void AddImage(IImage image);
    }
}

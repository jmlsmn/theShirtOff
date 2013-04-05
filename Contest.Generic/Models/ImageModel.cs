using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Abstract.Entities;

namespace Contest.Generic.Models
{
    public class ImageModel : IImage
    {
        public int ImageID { get; set; }

        public string ImageLocation { get; set; }

        public string ImageThumbnailLocation { get; set; }

        public string ImageMimeType { get; set; }
    }
}
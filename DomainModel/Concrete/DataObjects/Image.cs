using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel.Abstract.Entities;
using PetaPoco;

namespace DomainModel.Concrete.DataEntities
{
    [TableName("Images")]
    [PrimaryKey("ImageID")]
    internal class Image: IImage
    {
        public int ImageID { get; set; }

        public string ImageLocation { get; set; }

        public string ImageThumbnailLocation { get; set; }

        public string ImageMimeType { get; set; }
    }
}

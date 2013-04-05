using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Contest.Generic.Models
{
    public class UploadFileModel
    {
        public string Name { get; private set; } 
        public HttpPostedFileBase File { get; private set; }

        public UploadFileModel(string name, HttpPostedFileBase file) 
        { 
            Name = name; 
            File = file; 
        } 
    }
}
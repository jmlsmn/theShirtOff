using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Contest.Generic.Models;

namespace Contest.Generic.Binders
{
    public class UploadFilesBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var files = controllerContext.HttpContext.Request.Files;
            var list = new List<UploadFileModel>();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];

                if(file.ContentLength == 0)
                    continue;

                var name = files.AllKeys[i];
                var fileInfo = new UploadFileModel(name, file);
                list.Add(fileInfo);
            }

            return list.ToArray(); 
        }
    }
}
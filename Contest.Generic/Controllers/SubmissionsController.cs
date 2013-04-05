using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Abstract.Repositories;
using Castle.Core.Logging;
using DomainModel.Abstract.Services;
using Contest.Generic.Models;
using DomainModel.Abstract.Entities;
using Contest.Generic.Helpers;
using System.Text;
using System.Drawing;
using System.Configuration;


namespace Contest.Generic.Controllers
{
    public class SubmissionsController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ISubmissionService _submissionService;
        private readonly string _imagePath = ConfigurationManager.AppSettings["ImagePath"];
        private readonly string _imageThumbnailPath = ConfigurationManager.AppSettings["ImageThumbnailPath"];

        public SubmissionsController(IAccountService accountService, ISubmissionService submissionService)
        {
            _accountService = accountService;
            _submissionService = submissionService;
        }

        public ActionResult Submission(int submissionID)
        {
            var submission = _submissionService.GetSubmission(submissionID);
            return View(submission);
        }

        public ActionResult Submit()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Save(SubmissionModel submissionModel, UploadFileModel [] images)
        {
            List<IImage> submissionImages = new List<IImage>();
            int imageCounter = 0;
            foreach (var image in images)
            {
                ImageModel submissionImage = new ImageModel();
                submissionImage.ImageMimeType = image.File.ContentType;
                Image img = Image.FromStream(image.File.InputStream);

                

                string localImgPath = ControllerContext.HttpContext.Server.MapPath(string.Format("{0}{1}{2}{3}{4}",
                                                                                             _imagePath, 
                                                                                             CurrentAccount.AccountID,
                                                                                             CurrentContest.ContestID,
                                                                                             imageCounter,
                                                                                             ".jpg"));
                string imgPath = string.Format("{0}{1}{2}{3}{4}",
                                                _imagePath,
                                                CurrentAccount.AccountID,
                                                CurrentContest.ContestID,
                                                imageCounter,
                                                ".jpg");


                string localImgThumbnailPath = ControllerContext.HttpContext.Server.MapPath(string.Format("{0}{1}{2}{3}{4}",
                                                                                                    _imageThumbnailPath,
                                                                                                    CurrentAccount.AccountID,
                                                                                                    CurrentContest.ContestID,
                                                                                                    imageCounter,
                                                                                                    ".jpg"));
                string imgThumbnailPath = string.Format("{0}{1}{2}{3}{4}",
                                                        _imageThumbnailPath,
                                                        CurrentAccount.AccountID,
                                                        CurrentContest.ContestID,
                                                        imageCounter,
                                                        ".jpg");

                //Resize
                ImageUtilities.SaveImage(localImgPath, new Bitmap(img), 85L);
                submissionImage.ImageLocation = imgPath;

                //Resize to thumbnail

                Image imgThumbnail = img.GetThumbnailImage(210, 180,null,IntPtr.Zero);// ImageUtilities.ResizeImage(new Bitmap(img), new Size(210, 180));
                ImageUtilities.SaveImage(localImgThumbnailPath, new Bitmap(imgThumbnail), 85L);
                submissionImage.ImageThumbnailLocation = imgThumbnailPath;

                submissionImages.Add(submissionImage);
                imageCounter++;
            }

            submissionModel.SubmissionImages = submissionImages;
            submissionModel.SubmissionDate = DateTime.Now;
            submissionModel.AccountID = CurrentAccount.AccountID;
            submissionModel.ContestID = CurrentContest.ContestID;
            submissionModel.ApprovalDate = null;

            _accountService.AddSubmission(submissionModel);

            return View("Success");
        }

        //public FileContentResult GetSubmissionImage(int imageID)
        //{
        //    IImage image = _submissionService.GetSubmissionImage(imageID);
        //    if (image != null)
        //    {
        //        return File(image.ImageData, image.ImageMimeType);
        //    }
        //    return null;
        //}

        //public FileContentResult GetSubmissionThumbnail(int imageID)
        //{
        //    IImage image = _submissionService.GetSubmissionImage(imageID);
        //    if (image != null)
        //    {
        //        image.ImageData = ImageUtilities.ResizeToThumbnail(image.ImageData, 210);
        //        return File(image.ImageData, image.ImageMimeType);
        //    }
        //    return null;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Travel.Common;
using System.Drawing;

namespace Travel.WebAPI.Controllers
{
    [Authorize()]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Admin()
        {
            return View();
        }

        //GET: UploadedImages
        public ActionResult UploadImages()
        {
            ViewBag.Blogs = Business.Blog.SelectBlogs(false, true);
            return View();
        }

        [HttpPost]
        public ActionResult UploadImages(HttpPostedFileBase file, string blogID)
        {

            var files = new FileResult();

            var status = new List<ViewDataUploadFilesResult>();

            var r = new ViewDataUploadFilesResult();
            if (file.ContentLength != 0)
            {
                SaveFile(file, blogID);

                r.deleteType = "DELETE";
                r.deleteUrl = OriginalPath(file.FileName);
                r.url = OriginalPath(file.FileName);
                r.name = file.FileName;
                r.size = file.ContentLength;
                r.thumbnailUrl = ThumbnailPath(file.FileName);
                r.type = file.ContentType;
            }

            files.files = new ViewDataUploadFilesResult[] { r };

            return Json(files, JsonRequestBehavior.AllowGet);

        }

        [HttpDelete]
        public ActionResult UploadImages(string FileName, int ImageID)
        {
            try
            {
                System.IO.File.Delete(OriginalPath(FileName));
                System.IO.File.Delete(ThumbnailPath(FileName));
                System.IO.File.Delete(MediumPath(FileName));

                Business.ImageUpload.Delete(ImageID);

                return Json("Deleted");
            }
            catch
            {
                return Json("There was an error");
            }
        }

        public void SaveFile(HttpPostedFileBase file, string blogID)
        {

            string FileName = file.FileName.Replace(" ", "-");

            Image ThumbImage = Image.FromStream(file.InputStream);
            Image MediumImage = Image.FromStream(file.InputStream);

            //''Save the original file
            file.SaveAs(OriginalPath(FileName));

            //'Create Thumbnail
            ImageCompressor.CompressAndShrinkImage(ThumbnailPath(FileName), ThumbImage, 120, 80, 25);
            ImageCompressor.SaveJpeg(MediumPath(FileName), MediumImage, 50);

            ThumbImage.Dispose();
            MediumImage.Dispose();

            //''Save link to the file here in a database table so we can manage the pictures
            //''Save files to a database here so we can display the files in a grid.

            Nullable<int> id = int.Parse(blogID);
            if (id == 0) { id = null; };
            Business.ImageUpload.Insert(FileName, "", id);
        }


        public String OriginalPath(string FileName)
        {
            return Server.MapPath("~/images/uploads/original/") + FileName;
        }

        public String ThumbnailPath(string FileName)
        {
            return Server.MapPath("~/images/uploads/thumbnail/") + FileName;
        }

        public String MediumPath(string FileName)
        {
            return Server.MapPath("~/images/uploads/medium/") + FileName;
        }
    }


}
public class FileResult
{
    public ViewDataUploadFilesResult[] files { get; set; }
}


public class ViewDataUploadFilesResult
{
    public string deleteType { get; set; }
    public string deleteUrl { get; set; }
    public string name { get; set; }
    public string thumbnailUrl { get; set; }
    public string type { get; set; }
    public string url { get; set; }
    public int size { get; set; }
}
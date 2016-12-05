using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL.DAL;
using DLL.DAL.Entities;

namespace BookingApplicationWebApi.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase image, Image photo)
        {
            var path = "";

            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    if (Path.GetExtension(image.FileName).ToLower().Contains("jpg") ||
                        Path.GetExtension(image.FileName).ToLower().Contains("gif") ||
                        Path.GetExtension(image.FileName).ToLower().Contains("gif") ||
                        Path.GetExtension(image.FileName).ToLower().Contains("jpeg"))
                    {
                        photo.ImageFileName = image.FileName;
                        photo.ImageMimeType = image.ContentType;
                        photo.PhotoFile = new byte[image.ContentLength];
                        image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                    }
                    path = Path.Combine(Server.MapPath("~/Content/Images"), image.FileName);
                    BookingDbContext db = new BookingDbContext();

                    db.Images.Add(photo);
                    db.SaveChanges();
                    ViewBag.UploadSuccess = true;
                    return RedirectToAction("Upload");
                }
            }
            return View(image);
        }
    }
}
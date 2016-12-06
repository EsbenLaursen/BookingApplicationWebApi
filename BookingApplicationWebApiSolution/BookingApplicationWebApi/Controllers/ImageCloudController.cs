using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingApplicationWebApi.Models;
using CloudinaryDotNet.Actions;
using DLL.DAL;
using DLL.DAL.Entities;
using Newtonsoft.Json.Linq;

namespace BookingApplicationWebApi.Controllers
{
    public class ImageCloudController : Controller
    {
        static Cloudinary m_cloudinary;

        static ImageCloudController()
        {
            

            BookingDbContext album = new BookingDbContext();
            //album.Database.Initialize(false);

            Account acc = new Account(
                    "emildall",
                    "573275437216378",
                    "TNnMz8L7AWunpnUuQRfxVmGV12g");

            m_cloudinary = new Cloudinary(acc);
        }
        
        public ActionResult Index()
        {
            BookingDbContext album = new BookingDbContext();

            return PartialView("List", new PhotosModel(m_cloudinary, album.Images.ToList()));
        }

        public ActionResult Upload()
        {
            return PartialView("Upload", new Model(m_cloudinary));
        }
        
        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadServer()
        {
            BookingDbContext album = new BookingDbContext();
            Dictionary<string, string> results = new Dictionary<string, string>();

            for (int i = 0; i < HttpContext.Request.Files.Count; i++)
            {
                var file = HttpContext.Request.Files[i];

                if (file.ContentLength == 0) return PartialView("Upload", new Model(m_cloudinary));

                var result = m_cloudinary.Upload(new ImageUploadParams()
                {
                    File = new CloudinaryDotNet.Actions.FileDescription(file.FileName,
                        file.InputStream),
                    Tags = "backend_photo_album"
                });

                foreach (var token in result.JsonObj.Children())
                {
                    if (token is JProperty)
                    {
                        JProperty prop = (JProperty)token;
                        results.Add(prop.Name, prop.Value.ToString());
                    }
                }

                Image im = new Image()
                {
                    Bytes = (int)result.Length,
                    Path = result.Uri.AbsolutePath,
                    PublicId = result.PublicId,
                    SecureUrl = result.SecureUri.AbsoluteUri,
                    Type = result.JsonObj["type"].ToString(),
                    Url = result.Uri.AbsoluteUri,

                };

                album.Images.Add(im);
            }

            album.SaveChanges();

            return PartialView("UploadSucceeded",
                new DictionaryModel(m_cloudinary, results));
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public void UploadDirect()
        {
            var headers = HttpContext.Request.Headers;

            string content = null;
            using (StreamReader reader = new StreamReader(HttpContext.Request.InputStream))
            {
                content = reader.ReadToEnd();
            }

            if (String.IsNullOrEmpty(content)) return;

            Dictionary<string, string> results = new Dictionary<string, string>();

            string[] pairs = content.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var pair in pairs)
            {
                string[] splittedPair = pair.Split('=');

                if (splittedPair[0].StartsWith("faces"))
                    continue;

                results.Add(splittedPair[0], splittedPair[1]);
            }

            Image im = new Image()
            {
                Bytes = Int32.Parse(results["bytes"]),
                Path = results["path"],
                PublicId = results["public_id"],
                SecureUrl = results["secure_url"],
                Type = results["type"],
                Url = results["url"],
               };

            BookingDbContext album = new BookingDbContext();

            album.Images.Add(im);

            album.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL.DAL;
using DLL.DAL.Entities;
using WebGrease.Css.Extensions;

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
           
            List<Image> all = new List<Image>();
             //Chooses the BookingDbContext as our datacontext
                using (BookingDbContext dc = new BookingDbContext())
                {
                    all = dc.Images.ToList();
                 }
            

            return View(all);
        }

     
        public ActionResult Upload()
        {
            return View();
        }


       
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLL;
using DLL.DAL;
using DLL.DAL.Entities;
using WebGrease.Css.Extensions;
using DLL.DAL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    public class ImageController : Controller
    {
        private IRepository<Image> repo = new DllFacade().GetImageManager();

        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gallery()
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


        [HttpPost]
        public ActionResult Upload(Image IG)
        {
          
            
            //if (IG.File.ContentLength > (2*1024*1024))
            //{
            //    ModelState.AddModelError("CustomError", "File size must be less than 2 MEGAMEGABITSZ");
            //    return View();
            //}

            //if (!(IG.File.ContentType == "image/jpeg" || IG.File.ContentType == "image/png"))
            //{
            //    ModelState.AddModelError("CustomError", "File type must be Jpeg or png");
            //    return View();
            //}

            //IG.FileName = IG.File.FileName;
            //IG.ImageSize = IG.File.ContentLength;
            

            //byte[] data = new byte[IG.File.ContentLength];
            //IG.File.InputStream.Read(data, 0, IG.File.ContentLength);
                
            //repo.Create(IG);

            return RedirectToAction("Gallery");
        }

    }
}
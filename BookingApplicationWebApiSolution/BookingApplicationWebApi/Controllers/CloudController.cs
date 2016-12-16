using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using CloudinaryDotNet;
using DLL.DAL;
using DLL.DAL.Entities;
using DLL.DAL.Repositories;

namespace BookingApplicationWebApi.Controllers
{
    public class CloudController : ApiController
    {
        private IRepository<Image> repo = new DllFacade().GetImageManager();
      
           public List<String> GetAcc()
        {
            var list = new List<String>() {
           "emildall",
                "573275437216378",
                "TNnMz8L7AWunpnUuQRfxVmGV12g"};

            return list;
        }

    }
}

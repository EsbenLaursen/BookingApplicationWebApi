using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using BookingApplicationWebApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(BookingApplicationWebApi.Startup))]

namespace BookingApplicationWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser();
            user.UserName = "mor";
            user.Email = "mor";
            string userPWD = "lol123";

            var succes = UserManager.Create(user, userPWD);

            ConfigureAuth(app);
        }
    }
}

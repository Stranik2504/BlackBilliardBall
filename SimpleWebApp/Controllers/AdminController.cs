using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApp.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager _userManager;

        public AdminController(UserManager userManager) => _userManager = userManager;

        [HttpGet]
        [Route("adminPage")]
        public ActionResult Index()
        {
            var user = _userManager.GetUser(User.Identity.Name).Roles;

            if (user == CredentialsDto.Admin)
                return File(new FileStream(@"Site/adminPage.html", FileMode.Open), "text/html");
            else 
                return Unauthorized();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DeviceSensorWeb.Models;
using Microsoft.AspNetCore.Http;

namespace DeviceSensorWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LoginUser(User user)
        {
            TokenProvider _tokenProvider = new TokenProvider();
            //Authenticate user
            var userToken = _tokenProvider.LoginUser(user.USERID.Trim(), user.PASSWORD.Trim());
            if (userToken != null)
            {
                //Save token in session object
                HttpContext.Session.SetString("JWToken", userToken);
            }

            return Redirect("~/Home/Index");
        }
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");
        }
    }
}

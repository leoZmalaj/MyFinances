using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MyFinances.Contract.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFinances.Controllers
{
    public class LoginController : Controller
    {
        public readonly ILoginService LoginService;

        public LoginController(ILoginService loginService)
        {
            LoginService = loginService;
           
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> UserLogin(Models.Login model)
        {
          
            var reesult = await LoginService.LoginAsync(model);
            return Json(reesult);
        }   
        [HttpGet]
        public IActionResult LogOut()
        {
             LoginService.LogOutAsync();
             return Redirect("/Home");
        } 
        [HttpPost]
        public async Task<JsonResult> RegistUser(Models.Login model)
        {
            var reesult = await LoginService.RegisterAsync(model);
            return Json(reesult);
        }
    }
}

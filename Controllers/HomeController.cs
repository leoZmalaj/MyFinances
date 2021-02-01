using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MyFinances.Contract.Home;
using MyFinances.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinances.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IHomeService HomeService { get; set; }
        public readonly IMemoryCache MemoryChache;
        public HomeController(ILogger<HomeController> logger,IHomeService homeService, IMemoryCache memoryCache)
        {
            _logger = logger;
            HomeService = homeService;
            MemoryChache = memoryCache;
        }
       [AllowAnonymous]
        public IActionResult Index()
        {
            return View(0);
        } 
        [AllowAnonymous]
        public IActionResult ReturnLogin()
        {
            TempData["login"] = 1;
            return Redirect("/Home");
        }
        public IActionResult MyDashborard()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> FillBudgetTable()
        {
            
            var result = await HomeService.GetBudgetAsync(Convert.ToInt32(MemoryChache.Get("LogedUserId")));
            return Json(result);
        }
        [HttpGet]
        public async Task<JsonResult> DeleteFinance(int id)
        {
            var result = await HomeService.DeleteFinanceAsync(id);
            return Json(result);
        } 
        [HttpPost]
        public async Task<JsonResult> UpdateTable(Budget model)
        {
           
            var result = await HomeService.UpdateBudgetAsync(model);
            return Json(result);
        } 
        [HttpPost]
        public async Task<JsonResult> CreateNew(Budget model)
        {
            model.UserId =Convert.ToInt32(MemoryChache.Get("LogedUserId"));
            var result = await HomeService.CreateNewAsync(model);
            return Json(result);
        }

    }
}

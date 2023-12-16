using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCoffee.BL.IRepository;
using MyCoffee.DAL.Database;
using MyCoffee.Models;
using MyCoffee.Utility;

namespace MyCoffee.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]

    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IProductRep rep;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> manager;
        private readonly DBContext db;

        public HomeController(IProductRep rep, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> manager,DBContext db)
        {
            this.rep = rep;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.manager = manager;
            this.db = db;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId = manager.GetUserId(principal);
            return userId;
        }
        public IActionResult Index()
        {
            //string Photo = HttpContext.Session.Get<string>("Photo"); 
            //var user=  db.Identity.Where(a => a.Id == GetUserId()).Select(a => new {a.Photo});
            //foreach (var item in user)
            //{
            //    Photo=item.Photo;
            //    HttpContext.Session.Set("Photo", Photo);

                
            //}
             
            return View();
        }
       
      



    }
}

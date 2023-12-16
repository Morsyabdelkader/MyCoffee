using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCoffee.BL.Models;
using MyCoffee.DAL.Database;
using MyCoffee.DAL.Entites;
using MyCoffee.Models;
using MyCoffee.Utility;
using System.Security.Claims;

namespace MyCoffee.Controllers
{
    public class OrderController : Controller
    {
        private readonly DBContext db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> manager;

        public OrderController(DBContext db, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser>manager )
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
            this.manager = manager;
        }
        public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            string userId = manager.GetUserId(principal);
            return userId;
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Order model)
        {
            var totalprice = 0;
            List<ProductVM> products = HttpContext.Session.Get<List<ProductVM>>("Products");
            var user = GetUserId();

            model.UserId = user;

            
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        ProductId = product.Id,
                        Price=product.Price*product.Quantity,

                        Count=product.Quantity
                   
                    };
                    totalprice += product.Price * product.Quantity;

                    
                    model.orderdetail.Add(orderDetails);
                }
                model.TotalPrice = totalprice;
                db.Order.Add(model);
                await db.SaveChangesAsync();
                HttpContext.Session.Set("Products", new List<ProductVM>());
                return RedirectToAction("Index", "Home");
            }
           
            return View();
        }


    }
}

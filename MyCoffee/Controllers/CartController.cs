using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCoffee.BL.IRepository;
using MyCoffee.Models;
using MyCoffee.Utility;

namespace MyCoffee.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRep rep;
        private readonly IMapper mapper;

        public CartController(IProductRep rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }
        public IActionResult Details(ProductVM model)
        {
            var data = mapper.Map<ProductVM>(rep.GetById(model.Id));

            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [ActionName("Details")]
        public IActionResult Detailss(ProductVM model)
        {
            List<ProductVM> list = new List<ProductVM>();

            var data = mapper.Map<ProductVM>(rep.GetById(model.Id));
            data.Quantity = model.Quantity;

            list = HttpContext.Session.Get<List<ProductVM>>("Products");

            if (list == null)
            {
                list = new List<ProductVM>();
            }
            if (list.Any(x => x.Id == data.Id))
            {
                var itemId = list.Find(x => x.Id == data.Id);
                itemId.Quantity += model.Quantity;
                HttpContext.Session.Set("Products", list);

            }
            else
            {
                list.Add(data);
                HttpContext.Session.Set("Products", list);
            }

            return View(data);
        }
        [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id)
        {
            List<ProductVM> products = HttpContext.Session.Get<List<ProductVM>>("Products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("Products", products);
                }
            }
            return RedirectToAction("Cart");
        }
        public IActionResult Cart()
        {
            List<ProductVM> products = HttpContext.Session.Get<List<ProductVM>>("Products");
            if (products == null)
            {
                products = new List<ProductVM>();
            }
            return View(products);
        }
        [HttpPost]

        public IActionResult Remove(int? id)
        {
            List<ProductVM> products = HttpContext.Session.Get<List<ProductVM>>("Products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("Products", products);
                }

            }

            return RedirectToAction("Cart");
        }
        public IActionResult plus(ProductVM model)
        {
            List<ProductVM> list = new List<ProductVM>();

            var data = mapper.Map<ProductVM>(rep.GetById(model.Id));
            data.Quantity = model.Quantity;

            list = HttpContext.Session.Get<List<ProductVM>>("Products");

           
            if (list.Any(x => x.Id == data.Id))
            {
                var itemId = list.Find(x => x.Id == data.Id);
                itemId.Quantity += 1;
                HttpContext.Session.Set("Products", list);

            }
           

            return RedirectToAction("Cart");
        }
        public IActionResult mins(ProductVM model)
        {
            List<ProductVM> list = new List<ProductVM>();

            var data = mapper.Map<ProductVM>(rep.GetById(model.Id));
            data.Quantity = model.Quantity;
            list = HttpContext.Session.Get<List<ProductVM>>("Products");
            var itemId = list.Find(x => x.Id == data.Id);


            if (itemId.Id == data.Id)
            {
                if (itemId.Quantity <= 1)
                {
                    
                        var product = list.FirstOrDefault(c => c.Id == model.Id);
                        
                            list.Remove(product);
                            HttpContext.Session.Set("Products", list);
                        

                    }
                
                else {
                    itemId.Quantity -= 1;
                    HttpContext.Session.Set("Products", list);
                }

                

            }


            return RedirectToAction("Cart");
        }

    }
}

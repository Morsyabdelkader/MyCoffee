using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCoffee.BL.IRepository;
using MyCoffee.Models;
using MyCoffee.Utility;

namespace MyCoffee.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRep rep;
        private readonly IMapper mapper;

        public ProductController(IProductRep rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }
        public IActionResult Allproducts(string name)
        {
            if (name == null || name == "")
            {
                var data = mapper.Map<IEnumerable<ProductVM>>(rep.Get());
                return View(data);
            }
            else
            {
                var data = mapper.Map<IEnumerable<ProductVM>>(rep.GetByName(name));
                return View(data);
            }

        }
        public IActionResult productbycatid(int id)
        {
            var data = mapper.Map<IEnumerable<ProductVM>>(rep.GetByCatId(id));
            return View(data);
        }
        public IActionResult advanced()
        {
            return View();
        }
       
       
        public IActionResult advancedsearch(string name, string discription, int minprice, int maxprice, string casse)
        {
            var data = mapper.Map<IEnumerable<ProductVM>>(rep.Advancedsearch(name, discription, minprice, maxprice, casse));
            return View(data);
        }
    }
}

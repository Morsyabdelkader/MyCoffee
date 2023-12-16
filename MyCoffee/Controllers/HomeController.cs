using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCoffee.BL.IRepository;
using MyCoffee.DAL.Entites;
using MyCoffee.Models;
using MyCoffee.Utility;

namespace MyCoffee.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRep rep;
        private readonly IMapper mapper;

        public HomeController(IProductRep rep, IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            
            var data = mapper.Map<IEnumerable<ProductVM>>(rep.Get());
            return View(data);
        }
        public IActionResult about()
        {

            return View();
        }
        public IActionResult coffee()
        {
            return View();
        }
       

    }
}

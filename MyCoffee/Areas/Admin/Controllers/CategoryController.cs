using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCoffee.BL.IRepository;
using MyCoffee.BL.Models;
using MyCoffee.DAL.Entites;

namespace MyCoffee.Areas.Admin.Controllers

{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRep rep;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRep rep,IMapper mapper)
        {
            this.rep = rep;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            var data = mapper.Map<IEnumerable<CategoryVM>>(rep.GetAll());
            return View(data);
        }
        public IActionResult Create()
        {


            return View("_CreateCategory", new CategoryVM());

        }

        [HttpPost]
        public IActionResult Create(CategoryVM model)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    


                    var data = mapper.Map<Category>(model);

                    rep.Create(data);


                    return RedirectToAction("Index", "Category");
                }

                return View(model);

            }
            catch
            {


                return View(model);
            }


        }
        public IActionResult Edite(int id)
        {
            var data = mapper.Map<CategoryVM>(rep.GetById(id));
            return View(data);

        }

        [HttpPost]
        public IActionResult Edite(CategoryVM model)

        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    var data = mapper.Map<Category>(model);
                   
                    rep.Edite(data);
                    return RedirectToAction("Index", "Category");
                }
                return View(model);

            }
            catch
            {

                return View(model);
            }
        }
        public IActionResult Delete(int id)
        {

            var data = mapper.Map<CategoryVM>(rep.GetById(id));
            return View(data);

        }

        [HttpPost]
        public IActionResult Delete(CategoryVM model)

        {
            try
            {


                var data = mapper.Map<Category>(rep.GetById(model.Id));
                rep.Delete(data);

                return RedirectToAction("Index", "Category");


            }
            catch
            {


                return View(model);
            }
        }
    }
}

using AutoMapper;
using erp.BL.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCoffee.BL.IRepository;
using MyCoffee.BL.Models;
using MyCoffee.DAL.Entites;
using MyCoffee.Models;

namespace MyCoffee.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRep rep;
        private readonly IMapper mapper;
        private readonly ICategoryRep category;

        public ProductController(IProductRep rep, IMapper mapper, ICategoryRep category)
        {
            this.rep = rep;
            this.mapper = mapper;
            this.category = category;
        }
        public IActionResult Index()
        {
            ViewBag.CategoryList = new SelectList(category.GetAll(), "Id", "Name");
            
            var data = mapper.Map<IEnumerable<ProductVM>>(rep.Get());
            var model = new IndexCreateVM
            {
                Products = data,
                Categories = mapper.Map<IEnumerable<CategoryVM>>(category.GetAll()),
            };
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(category.GetAll(), "Id", "Name");



            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Create(IndexCreateVM model)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PhotoName = FileUploader.UploadFile("Photo/", model.Product.PhotoUrl);


                    var data = mapper.Map<Product>(model.Product);
                    data.Photo = PhotoName;

                    rep.create(data);


                    return RedirectToAction("Index", "Product");
                }
                ViewBag.CategoryList = new SelectList(category.GetAll(), "Id", "Name");

                return View(model.Product);

            }
            catch
            {
                ViewBag.CategoryList = new SelectList(category.GetAll(), "Id", "Name");


                return View(model.Product);
            }


        }
        public IActionResult Edite(int id)
        {
            ViewBag.CategoryList = new SelectList(category.GetAll(), "Id", "Name");
            var data = mapper.Map<ProductVM>(rep.GetById(id));
            return View(data);

        }

        [HttpPost]
        public IActionResult Edite(ProductVM model)

        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PhotoName = FileUploader.UploadFile("Photo/", model.PhotoUrl);
                    var data = mapper.Map<Product>(model);
                    data.Photo = PhotoName;
                    rep.Edite(data);
                    return RedirectToAction("Index", "Product");
                }
                return View(model);

            }
            catch
            {
                ViewBag.CategoryList = new SelectList(category.GetAll(), "Id", "Name");

                return View(model);
            }
        }
        public IActionResult Delete(int id)
        {
            ViewBag.CategoryList = new SelectList(category.GetAll(), "Id", "Name");

            var data = mapper.Map<ProductVM>(rep.GetById(id));
            return View(data);

        }

        [HttpPost]
        public IActionResult Delete(ProductVM model)

        {
            try
            {
                
                   
                    var data = mapper.Map<Product>(rep.GetById(model.Id));
             var mes= FileUploader.RemoveFile("/Photo/", data.Photo);
                rep.Delete(data);
                    
                    return RedirectToAction("Index", "Product");
               

            }
            catch
            {
              

                return View(model);
            }
        }
    }
}

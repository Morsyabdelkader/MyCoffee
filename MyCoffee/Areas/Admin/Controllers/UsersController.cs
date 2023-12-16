using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCoffee.BL.Models;
using MyCoffee.DAL.Database;

namespace MyCoffee.Areas.Admin.Controllers
{   [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly DBContext db;
        private readonly UserManager<IdentityUser> usermanager;
        private readonly RoleManager<IdentityRole> role;
        private readonly IMapper mapper;

        public UsersController(DBContext db,UserManager<IdentityUser> usermanager,RoleManager<IdentityRole> role,IMapper mapper)
        {
            this.db = db;
            this.usermanager = usermanager;
            this.role = role;
            this.mapper = mapper;
        }
        public async Task<IActionResult>  Index()
        {
            var usersWithRoles = db.Identity
         .Select(u => new UserVM
         {
            Id= u.Id,
            UserName= u.UserName,
            Email= u.Email,
            Address= u.Address,
            Photo= u.Photo,
             Roles = db.UserRoles
                 .Where(ur => ur.UserId == u.Id)
                 .Join(db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                 .ToList()
         })
         .ToList();

            return View(usersWithRoles);
            //var users =  db.Identity.Select(user => new UserVM { Id = user.Id, Email = user.Email, UserName = user.UserName,Photo=user.Photo,Address=user.Address, Roles = usermanager.GetRolesAsync(user).Result }).AsAsyncEnumerable();
            
            //return View(users);
        }
        public async Task<IActionResult> RoleList()
        {
            var data = role.Roles.ToList(); 

            return View(data);
        }
        public async Task<IActionResult> Create()
        {
           

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var data = mapper.Map<IdentityRole>(model);
                    var isExist = await role.RoleExistsAsync(model.Name);
                    if (isExist) {
                        ModelState.AddModelError("", " This role is aldeady exists");
                        return View(model);
                    }
                    await role.CreateAsync(data);
                    
                    return RedirectToAction("RoleList");
                }
                
                   
               
                    
                

                return View(model);

            }
            catch
            {


                return View(model);
            }
          

           
        }
        public async Task<IActionResult> Edite(string id)
        {
            var data = await role.FindByIdAsync(id);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Edite(string id,string name)
        {
            try
            {
                var r = await role.FindByIdAsync(id);
                if (r == null)
                {
                    return NotFound();
                }
                r.Name = name;
                var isExist = await role.RoleExistsAsync(r.Name);
                if (isExist)
                {
                    ViewBag.mgs = "This role is aldeady exist";
                    
                    return View();
                }
                var result = await role.UpdateAsync(r);
                if (result.Succeeded)
                {
                    TempData["save"] = "Role has been updated successfully";
                    return RedirectToAction("RoleList");
                }
                return View();





                

            }
            catch
            {


                return View();
            }



        }
        public async Task< IActionResult> Delete(string id)
        {

            var data =await role.FindByIdAsync(id);
            return View(data);

        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Deletee(string id)

        {
            try
            {


                var data =await role.FindByIdAsync(id);
               await role.DeleteAsync(data);

                return RedirectToAction("RoleList", "Users");


            }
            catch
            {


                return View();
            }
        }
        public async Task<IActionResult> Assign()
        {
            ViewBag.UserList = new SelectList(db.Identity.Select(a=>a), "Id", "UserName");
            ViewBag.RoleList = new SelectList(role.Roles, "Name", "Name");

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Assign(RoleUserVM model)
        {
            var user = db.Identity.FirstOrDefault(c => c.Id == model.UserId);
            var isCheckRoleAssign = await usermanager.IsInRoleAsync(user, model.RoleId);
            if (isCheckRoleAssign)
            {
                ModelState.AddModelError("", "This user already assign this role.");
                ViewBag.UserList = new SelectList(db.Identity.Select(a => a), "Id", "UserName");
                ViewBag.RoleList = new SelectList(role.Roles, "Name", "Name");
                return View();
            }
            var r = await usermanager.AddToRoleAsync(user, model.RoleId);
            
            if (r.Succeeded)
            { 
                
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        public async Task<IActionResult> RemoveRole()
        {
            ViewBag.UserList = new SelectList(db.Identity.Select(a => a), "Id", "UserName");
            ViewBag.RoleList = new SelectList(role.Roles, "Name", "Name");

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> RemoveRole(RoleUserVM model)
        {
            var user = db.Identity.FirstOrDefault(c => c.Id == model.UserId);
            var isCheckRoleAssign = await usermanager.IsInRoleAsync(user, model.RoleId);
            if (isCheckRoleAssign)
            {
                var r = await usermanager.RemoveFromRoleAsync(user, model.RoleId);

                if (r.Succeeded)
                {

                    return RedirectToAction(nameof(Index));
                }
              
            }
            ModelState.AddModelError("", "This user Not assign this role.");
            ViewBag.UserList = new SelectList(db.Identity.Select(a => a), "Id", "UserName");
            ViewBag.RoleList = new SelectList(role.Roles, "Name", "Name");
            

            return View();

        }
    }
}

using erp.BL.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCoffee.BL.Models;
using MyCoffee.DAL.Database;
using MyCoffee.DAL.Entites;
using System.Security.Claims;

namespace MyCoffee.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> usermanager;
        private readonly SignInManager<IdentityUser> manager;
        private readonly DBContext db;


        public AccountController(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> manager, DBContext db, RoleManager<IdentityRole> role)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.usermanager = usermanager;
            this.manager = manager;
            this.db = db;

        }
        public IActionResult SignUp()
        {

            return View();

        }

        public IActionResult SignOut()
        {
            manager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        

        [HttpPost]
        public async Task<IActionResult> SignUp(RegistrationVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PhotoName = FileUploader.UploadFile("Photo/", model.PhotoUrl);
                    var user = new Identity()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        Address2 = model.Address2,
                        City = model.City,
                        Email = model.Email,
                        UserName = model.Username,
                        State = model.State,
                        Zip = model.Zip,
                        Photo = PhotoName,
                    };
                    //var user = new IdentityUser()
                    //{

                    //    Email = model.Email,
                    //    UserName = model.Username,

                    //};
                    var result = await usermanager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private string GetUserId()
        {
            var principal = httpContextAccessor.HttpContext.User;
            var user = usermanager.GetUserId(principal);
            
            return user;
        }
        //public IActionResult SignIn()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> SignIn(LoginVM model)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {



        //            var result = await manager.PasswordSignInAsync(model.Username, model.Password, model.rememberme, false);
        //            // Example: Set photo claim during login



        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "invalid password or username");
        //                return View();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View();
        //}

        public IActionResult SignIn(string returnUrl= null)
        {   if(returnUrl != null) { 
            ViewData["ReturnUrl"] = returnUrl;
            return View();
            }
            else { return View(); }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM model, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await manager.PasswordSignInAsync(model.Username, model.Password, model.rememberme, false);

                    if (result.Succeeded)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid password or username");
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }

            return View();
        }
        public async Task<IActionResult> Profile(ProfileVM m)
        {
            var user = await usermanager.GetUserAsync(User);
            var u = db.Identity.Where(a => a.Id == user.Id).FirstOrDefault();

            var model = new ProfileVM
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Address = u.Address,
                Address2 = u.Address2,
                City = u.City,
                Username = u.UserName,
                State = u.State,
                Zip = u.Zip,



            };


            return View(model);
        }
        [ActionName("Profile")]
        [HttpPost]
        public async Task<IActionResult> Profilee(ProfileVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var PhotoName = FileUploader.UploadFile("Photo/", model.PhotoUrl);

                    var user = await usermanager.GetUserAsync(User);
                    var u = db.Identity.Where(a => a.Id == user.Id).FirstOrDefault();


                    u.FirstName = model.FirstName;
                    u.LastName = model.LastName;
                    u.Email = model.Email;
                    u.Address = model.Address;
                    u.Address2 = model.Address2;
                    u.City = model.City;
                    u.UserName = model.Username;
                    u.State = model.State;
                    u.Zip = model.Zip;
                    u.Photo = PhotoName;


                    var result = await usermanager.UpdateAsync(u);
                    if (model.oldpassword != null || model.Password != null)
                    {
                        var res = await usermanager.ChangePasswordAsync(u, model.oldpassword, model.Password);
                        if (res.Succeeded)
                        {
                            await manager.SignOutAsync();
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            foreach (var item in res.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                            return View(model);
                        }
                    }
                    else
                    {
                        var res = await usermanager.ChangePasswordAsync(u, "", "");
                        return View(model);


                    }

                }
                return RedirectToAction("Profile");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }







        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,new
                AuthenticationProperties
            {
                RedirectUri =Url.Action("GoogleResponse")
            });

        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.Value,
                claim.Type,
                claim.OriginalIssuer
            });
            return Json(claims);
        }

    }
}

using Demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
			UserManager = userManager;
			this.signInManager = signInManager;
		}
		
		private readonly UserManager<ApplicationUser> UserManager;
		private readonly SignInManager<ApplicationUser> signInManager;
		#region Register
		[HttpGet]
		public IActionResult Register()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split('@')[0],
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,
                };
                var Result = await UserManager.CreateAsync(User, model.Password);

                if (Result.Succeeded)
                    return RedirectToAction("Login");
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }


            }
            return View(model);

        }

        #endregion
        [HttpGet]
        public IActionResult login() {
           return View();
        }
        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user =await UserManager.FindByEmailAsync(model.Email);

                if (user is not null) 
                {
					var res = await UserManager.CheckPasswordAsync(user, model.Password);
                    if(res)
                    {
                      var result=await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                            return RedirectToAction("index", "Home");

                    }
                    else 
                    {
						ModelState.AddModelError(string.Empty, "Password is incorrect");
					}
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email doesn't exsist");
                }

            }
            return View(model);
        }
        public new async Task<IActionResult> SignOut() { 
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(login));
        
        }
        public IActionResult ForgetPassword() 
        { 
        return View();
        }
        public async Task<IActionResult> SendEmail(ForgetPassword model) 
        {
            if (ModelState.IsValid)
            {
                
                var User=await UserManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var _token=await UserManager.GeneratePasswordResetTokenAsync(User);
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new {email = model.Email,Token=_token},Request.Scheme);
                    var Email = new Email()
                    {
                        subject = "Reset Password",
                        To = model.Email,
                        body = "ResetPasswordLink"
                    };
                    EmailSettings.SendEmail(Email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                    ModelState.AddModelError(string.Empty, "Email doesn't exsist");

            }
           
                return View("ForgetPassword",model);
        }
        public IActionResult CheckYourInbox() 
        {
            return View();
        }
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var User=await UserManager.FindByEmailAsync(email);
                var res=await UserManager.ResetPasswordAsync(User,token,resetPassword.NewPassword);
                if(res.Succeeded) 
                    return RedirectToAction(nameof(login));
                else
                    foreach(var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty,error.Description);
                    }
            }
            return View();
        }
    }
}

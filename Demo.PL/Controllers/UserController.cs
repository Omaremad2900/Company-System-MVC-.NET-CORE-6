using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		public UserController(UserManager<ApplicationUser> userManager) 
		{
			this.userManager = userManager;
		}
		public async Task<IActionResult> Index(string SearchValue)
		{
			if (string.IsNullOrEmpty(SearchValue))
			{
				var Users =await userManager.Users.Select(U=>new UserViewModel()
				{
					id = U.Id,
					FName = U.UserName,
					LName = U.UserName,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles=userManager.GetRolesAsync(U).Result
				}).ToListAsync();
				return View(Users);
			}
			else
			{
				var User =await userManager.FindByNameAsync(SearchValue);
				var MappedUser = new UserViewModel()
				{
					id = User.Id,
					FName = User.UserName,
					LName = User.UserName,
					Email = User.Email,
					PhoneNumber = User.PhoneNumber,
					Roles = userManager.GetRolesAsync(User).Result
				};
				return View(new List<UserViewModel> { MappedUser});
			}
			
		}
	}
}

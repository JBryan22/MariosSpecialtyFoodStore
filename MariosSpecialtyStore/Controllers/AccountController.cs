using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MariosSpecialtyStore.Models;
using System.Threading.Tasks;
using MariosSpecialtyStore.ViewModels;

namespace MariosSpecialtyStore.Controllers
{
	public class AccountController : Controller
	{
		private readonly MariosSpecialtyStoreContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MariosSpecialtyStoreContext db)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_db = db;
		}

		public IActionResult Index()
		{
			return View();
		}

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, isPersistent: true, lockoutOnFailure: false
            );
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
	}
}
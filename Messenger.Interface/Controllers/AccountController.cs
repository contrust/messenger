using System.Threading.Tasks;
using Messenger.Interface.Database;
using Messenger.Interface.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Messenger.Domain.Models;

namespace Messenger.Interface.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<Models.User> userManager { get; }
        private SignInManager<Models.User> signInManager { get; }
        private ApplicationDbContext dbContext { get; }

        public AccountController(
            UserManager<Models.User> userManager,
            SignInManager<Models.User> signInManager,
            ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new Models.User
            {
                UserName = username
            };
            if (password != null)
            {
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    dbContext.DomainUsers.Add(new Domain.Models.User { Name = username });
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Register", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
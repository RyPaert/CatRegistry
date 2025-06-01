using CatRegistry.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CatRegistry.Controllers
{
    public class AccountsController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var users = new List<(string Name, string Password)>
            {
                ("admin", "admin"),
                ("player", "player")
            };
            foreach (var user in users)
            {
                if (user.Name == loginViewModel.Name && user.Password == loginViewModel.Password)
                {
                    HttpContext.Session.SetString("Name", user.Name); ;

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(loginViewModel);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Accounts");
        }
    }
}

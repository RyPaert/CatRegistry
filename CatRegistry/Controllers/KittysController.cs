using Microsoft.AspNetCore.Mvc;

namespace CatRegistry.Controllers
{
    public class KittysController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

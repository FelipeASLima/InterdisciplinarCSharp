using Microsoft.AspNetCore.Mvc;

namespace Inter.Controllers
{
    public class AdministradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }

}
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCore.ClientValidation.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                return Redirect(Url.Action("Index", "Home"));
            }

            return View(model);
        }

    }
}

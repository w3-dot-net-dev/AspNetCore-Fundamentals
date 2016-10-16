namespace AspNetCore.GlobalizationAndLocalization.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Localization;
    using System;

    public class HomeController : Controller
    {

        private readonly IStringLocalizer _localizer;
        private readonly IStringLocalizer<HomeController> _localizerHomeController;
        private readonly IStringLocalizerFactory _localizerFactory;

        public HomeController(IServiceProvider locator)
        {
            _localizer = locator.GetService<IStringLocalizer>();
            //_localizer = locator.GetService<IStringLocalizerFactory>()?.Create(typeof());
            _localizerHomeController = locator.GetService<IStringLocalizer<HomeController>>();

            var aboutTitle = _localizer?["About Title"];
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

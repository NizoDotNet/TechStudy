using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using TechStudy.Models;
using TechStudy.Options;

namespace TechStudy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<EmailOption> _emailOption;
        public HomeController(ILogger<HomeController> logger, IOptions<EmailOption> emailOption)
        {
            _logger = logger;
            _emailOption = emailOption;
        }

        public IActionResult Index()
        {
            EmailOption emailOption = _emailOption.Value;
            return View(emailOption);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

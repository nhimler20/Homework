using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sample2.Models;
using System.Linq;

namespace Sample2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new NamesModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AssignTeams(NamesModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var namesList = model.Names.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var teams = AssignNamesToTeams(namesList, model.NumberOfTeams);
            return View("Teams", teams);
        }

        private Dictionary<string, List<string>> AssignNamesToTeams(List<string> names, int numberOfTeams)
        {
            var teams = new Dictionary<string, List<string>>();
            for (int i = 0; i < numberOfTeams; i++)
            {
                teams[$"Team {i + 1}"] = new List<string>();
            }

            var random = new Random();
            var shuffledNames = names.OrderBy(x => random.Next()).ToList();

            for (int i = 0; i < shuffledNames.Count; i++)
            {
                teams[$"Team {(i % numberOfTeams) + 1}"].Add(shuffledNames[i]);
            }

            return teams;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

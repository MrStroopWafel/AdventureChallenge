using Microsoft.AspNetCore.Mvc;

namespace AdventureChallenge.Controllers
{
    public class CommunitymedewerkerController : Controller
    //CommunitymedewerkerController : HomeController  Hoe maak ik CommunitymedewerkerController een child van de HomeController. Ik kan alleen een child zijn van de Controller anders die die het niet.
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

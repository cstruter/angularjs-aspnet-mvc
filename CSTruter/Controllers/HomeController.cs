using CSTruter.Models;
using CSTruter.Models.Repositories.Interfaces;
using System.Web.Mvc;

namespace CSTruter.Controllers
{
    public class HomeController : Controller
    {
        IPeopleRepository _peopleRepository;
        public HomeController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            populateGenders();
            return View();
        }

        [HttpPost]
        public ActionResult Edit(PersonViewModel model)
        {
            populateGenders();
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            return RedirectToAction("Index");
        }

        private void populateGenders()
        {
            ViewBag.Genders = new[] {
                new { id = 1, text = "Male" },
                new { id = 2, text = "Female" }
            };
        }
    }
}
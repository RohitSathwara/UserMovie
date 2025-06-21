using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UserMovie.Models.ViewModel;
using UserMovie.Services;

namespace UserMovie.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Movies = _movieService.GetAll();
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        public IActionResult Add(MovieViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _movieService.Add(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _movieService.GetById(id);
            if (model == null)
                return NotFound();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(MovieViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _movieService.Update(model);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");

            _movieService.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteMultiple(List<int> selectedMovieIds)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToAction("Login", "Account");

            if (selectedMovieIds != null && selectedMovieIds.Any())
            {
                _movieService.DeleteMultiple(selectedMovieIds);
            }

            return RedirectToAction("Index");
        }
    }
}

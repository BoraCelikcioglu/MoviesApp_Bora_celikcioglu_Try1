using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.DAL;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    
    public class MoviesController : MvcController
    {
        // Service injections:
        //private readonly IMoviesService _moviesService;
        private readonly IDirectorsService _directorService;
        private readonly IService<Movie,MoviesModel> _moviesService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        private readonly IService<Genre,GenreModel> _genreService;

        public MoviesController(
            IService<Movie, MoviesModel> moviesService
            //IMoviesService movieService
            , IDirectorsService directorService

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        , IService<Genre, GenreModel> genreService
        )
        {
            _moviesService = moviesService;
            _directorService = directorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            _genreService = genreService;
        }

        // GET: Movies
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _moviesService.Query().ToList();
            return View(list);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _moviesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Record.Id", "FullName");

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            ViewBag.GenreIds = new MultiSelectList(_genreService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]

        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        public IActionResult Create(MoviesModel movie)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _moviesService.Create(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _moviesService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Movies/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(MoviesModel movie)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _moviesService.Update(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(int id)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "Users");
            }
            // Get item to delete service logic:
            var item = _moviesService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _moviesService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}

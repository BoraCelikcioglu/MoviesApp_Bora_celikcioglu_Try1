using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class DirectorsController : MvcController
    {
        // Service injections:
        private readonly IDirectorsService _directorsService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public DirectorsController(
			IDirectorsService directorService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _directorsService = directorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Directors
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _directorsService.Query().ToList();
            return View(list);
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _directorsService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //ViewBag.ManyToManyRecordIds = new MultiSelectList(_ManyToManyRecordService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorsModel director)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _directorsService.Create(director.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = director.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(director);
        }

        // GET: Directors/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _directorsService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Directors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectorsModel director)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _directorsService.Update(director.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = director.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(director);
        }

        // GET: Directors/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _directorsService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Directors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _directorsService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}

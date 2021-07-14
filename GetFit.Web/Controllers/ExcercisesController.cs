using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GetFit.Domain.Models;
using GetFit.Infrastructure;
using GetFit.Infrastructure.Repositories;
using System.Collections.Generic;
using GetFit.Infrastructure.SearchSortFilter;

namespace GetFit.Web.Controllers
{
    public class ExcercisesController : Controller
    {
        private readonly IRepository<Excercise> _repository;

        public List<Excercise> Ordered { get; set; }
        public IEnumerable<Excercise> Excercises { get; set; }

        public ExcercisesController(IRepository<Excercise> repository)
        {
            _repository = repository;
        }

        // GET: Excercises
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "name";
            ViewData["MuscleGroupSortParm"] = sortOrder == "muscleGroup" ? "muscleGroup_desc" : "muscleGroup";

            if (!string.IsNullOrEmpty(sortOrder))
            {
                ViewData["CurrentFilter"] = currentFilter;
            }
            else
            {
                ViewData["CurrentFilter"] = searchString;
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                Excercises = _repository.GetAll()
                    .Where(w => w.Name.Contains(searchString)
                             || w.Description.Contains(searchString))
                    .Distinct();
            }
            else
            {
                Excercises = _repository.GetAll();
            }

            switch (sortOrder)
            {
                case "name":
                    Ordered = Excercises.OrderBy(e => e.Name).ToList();
                    //return View(Ordered);
                    break;

                case "name_desc":
                    Ordered = Excercises.OrderByDescending(e => e.Name).ToList();
                    //return View(Ordered);
                    break;

                case "muscleGroup":
                    Ordered = Excercises.OrderBy(e => e.MuscleGroup).ToList();
                    //return View(Ordered);
                    break;

                case "muscleGroup_desc":
                    Ordered = Excercises.OrderByDescending(e => e.MuscleGroup).ToList();
                    //return View(Ordered);
                    break;

                default:
                    Ordered = Excercises.OrderBy(e => e.Name).ToList();
                    //return View(Ordered);
                    break;
            }

            int pageSize = 30;

            var paginatedList = await PaginatedList<Excercise>.CreateAsync(Ordered, pageNumber ?? 1, pageSize);

            return View(paginatedList);
          
        }

        // GET: Excercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = _repository.GetById(id);

            if (excercise == null)
            {
                return NotFound();
            }

            return View(excercise);
        }

        // GET: Excercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Excercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MuscleGroup,Description")] Excercise excercise)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(excercise);

                _repository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(excercise);
        }

        // GET: Excercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {


            return View(_repository.GetById(id));

            if (id == null)
            {
                return NotFound();
            }

            var excercise = _repository.GetById(id);
            if (excercise == null)
            {
                return NotFound();
            }
            return View(excercise);
        }

        // POST: Excercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MuscleGroup,Description")] Excercise excercise)
        {
            _repository.Edit(excercise);
            _repository.SaveChanges();

            return RedirectToAction("Index");


            //if (id != excercise.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(excercise);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ExcerciseExists(excercise.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(excercise);
        }

        // GET: Excercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = _repository.GetById(id);
            if (excercise == null)
            {
                return NotFound();
            }

            return View(excercise);
        }

        // POST: Excercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var excercise = _repository.GetById(id);

            _repository.Remove(excercise);
            _repository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool ExcerciseExists(int id)
        {
            return _repository.GetAll().Any(e => e.Id == id);
        }
    }
}

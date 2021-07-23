using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GetFit.Domain.Models;
using GetFit.Infrastructure.Repositories;
using System.Collections.Generic;
using GetFit.Infrastructure.SearchSortFilter;

namespace GetFit.Web.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IRepository<Workout> _repository;

        public List<Workout> Ordered { get; set; }
        public IQueryable<Workout> Ordered2 { get; set; }
        public IEnumerable<Workout> Workouts { get; set; }

        public WorkoutsController(IRepository<Workout> repository)
        {
            _repository = repository;
        }

        // GET: Workouts
        // GET: Excercises
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescriptionSortParm"] = sortOrder == "description" ? "description_desc" : "description";
            ViewData["NumberOfExcercisesSortParm"] = sortOrder == "excercises" ? "excercises_desc" : "excercises";

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
                var workouts = await _repository.GetAll();

                Workouts = workouts.Where(w => w.Name.ToUpper().Contains(searchString.ToUpper())
                             || w.Description.Contains(searchString));                  
            }
            else
            {
                Workouts = await _repository.GetAll();
            }

            Ordered = sortOrder switch
            {
                "name_desc" => Workouts.OrderByDescending(e => e.Name).ToList(),
                "description" => Workouts.OrderBy(e => e.Description).ToList(),
                "description_desc" => Workouts.OrderByDescending(e => e.Description).ToList(),
                "excercises" => Workouts.OrderBy(e => e.Excercises.Count).ToList(),
                "excercises_desc" => Workouts.OrderByDescending(e => e.Excercises.Count).ToList(),
                _ => Workouts.OrderBy(e => e.Name).ToList(),
            };
            int pageSize = 15;

            var paginatedList = await PaginatedList<Workout>.CreateAsync(Ordered2, pageNumber ?? 1, pageSize);
            
            return View(paginatedList);

        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _repository.GetById(id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(workout);
                _repository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _repository.GetById(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Workout workout)
        {
            if (id != workout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(workout);
                    await _repository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await WorkoutExistsAsync(workout.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }

        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = _repository.GetById(id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _repository.GetById(id);
            _repository.Remove(workout);
            await _repository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkoutExistsAsync(int id)
        {
            var workout = await _repository.GetAll();

            return workout.Any(w => w.Id == id);
        }
    }
}

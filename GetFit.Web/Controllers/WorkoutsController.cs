using GetFit.Domain.Models;
using GetFit.Infrastructure.Repositories;
using GetFit.Infrastructure.SearchSortFilter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetFit.Web.Controllers
{
    public class WorkoutsController : Controller
    {
        private readonly IRepository<Workout> _repository;

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
            ViewData["MuscleGroupSortParm"] = sortOrder == "muscleGroup" ? "muscleGroup_desc" : "muscleGroup";
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var workouts = _repository.GetAllAsQuery();

            if (!string.IsNullOrEmpty(searchString))
            {
                Workouts = await _repository.Find(
                    w => w.Name.Contains(searchString) ||
                    w.Description.Contains(searchString));
            }
            else
            {
                Workouts = await _repository.GetAll();
            }

            IEnumerable<Workout> newList = sortOrder switch
            {
                "name_desc" => Workouts.OrderByDescending(e => e.Name),
                "muscleGroup" => Workouts.OrderBy(e => e.Description),
                "muscleGroup_desc" => Workouts.OrderByDescending(e => e.Description),
                _ => Workouts.OrderBy(e => e.Name),
            };
            int pageSize = 30;
            try
            {
                return View(PaginatedList<Workout>.Create(newList, pageNumber ?? 1, pageSize));

            }
            catch (System.Exception e)
            {
                return View(nameof(Index));
            }

        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _repository.GetById(id);
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
                await _repository.Add(workout);
                await _repository.SaveChanges();
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

            var workout = await _repository.GetById(id);
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

            var workout = await _repository.GetById(id);
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

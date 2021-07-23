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
    public class WorkoutProgramsController : Controller
    {
        private readonly IRepository<WorkoutProgram> _repository;
        public List<WorkoutProgram> Ordered { get; set; }
        public IQueryable<WorkoutProgram> Ordered2 { get; set; }

        public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }

        public WorkoutProgramsController(IRepository<WorkoutProgram> repository)
        {
            _repository = repository;
        }

        // GET: WorkoutPrograms
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

            var workoutPrograms = _repository.GetAllAsQuery();

            if (!string.IsNullOrEmpty(searchString))
            {
                workoutPrograms = workoutPrograms
                    .Where(w => w.Name.ToUpper().Contains(searchString.ToUpper())
                             || w.Description.Contains(searchString))
                    .Distinct();
            }

            IOrderedQueryable<WorkoutProgram> newList = sortOrder switch
            {
                "name_desc" => workoutPrograms.OrderByDescending(e => e.Name),
                "muscleGroup" => workoutPrograms.OrderBy(e => e.Description),
                "muscleGroup_desc" => workoutPrograms.OrderByDescending(e => e.Description),
                _ => workoutPrograms.OrderBy(e => e.Name),
            };
            int pageSize = 30;
            try
            {
                return View(await PaginatedList<WorkoutProgram>.CreateAsync(newList.AsNoTracking(), pageNumber ?? 1, pageSize));

            }
            catch (System.Exception e)
            {
                return View(nameof(Index));
            }

        }

        // GET: WorkoutPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _repository.GetById(id);
            if (workoutProgram == null)
            {
                return NotFound();
            }

            return View(workoutProgram);
        }

        // GET: WorkoutPrograms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] WorkoutProgram workoutProgram)
        {
            if (ModelState.IsValid)
            {
               await _repository.Add(workoutProgram);
               await _repository.SaveChanges();
               return RedirectToAction(nameof(Index));
            }
            return View(workoutProgram);
        }

        // GET: WorkoutPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _repository.GetById(id);
            if (workoutProgram == null)
            {
                return NotFound();
            }
            return View(workoutProgram);
        }

        // POST: WorkoutPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] WorkoutProgram workoutProgram)
        {
            if (id != workoutProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(workoutProgram);
                    await _repository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await WorkoutProgramExistsAsync(workoutProgram.Id))
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
            return View(workoutProgram);
        }

        // GET: WorkoutPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _repository.GetById(id);
            if (workoutProgram == null)
            {
                return NotFound();
            }

            return View(workoutProgram);
        }

        // POST: WorkoutPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutProgram = await _repository.GetById(id);
            _repository.Remove(workoutProgram);
            await _repository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkoutProgramExistsAsync(int id)
        {
            var workoutProgram = await _repository.GetAll();

            return workoutProgram.Any(e => e.Id == id);
        }
    }
}

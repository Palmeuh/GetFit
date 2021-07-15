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
        IRepository<WorkoutProgram> _repository;
        public List<WorkoutProgram> Ordered { get; set; }
        public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }

        public WorkoutProgramsController(IRepository<WorkoutProgram> repository)
        {
            _repository = repository;
        }

        // GET: WorkoutPrograms
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DescriptionSortParm"] = sortOrder == "description" ? "description_desc" : "muscleGroup";
            ViewData["NumberOfWorkoutsSortParm"] = sortOrder == "workouts" ? "workouts_desc" : "workouts";


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
                WorkoutPrograms = _repository.GetAll()
                    .Where(w => w.Name.ToUpper().Contains(searchString.ToUpper())
                             || w.Description.Contains(searchString))
                    .Distinct();
            }
            else
            {
                WorkoutPrograms = _repository.GetAll();
            }

            switch (sortOrder)
            {
                

                case "name_desc":
                    Ordered = WorkoutPrograms.OrderByDescending(e => e.Name).ToList();
                    //return View(Ordered);
                    break;

                case "muscleGroup":
                    Ordered = WorkoutPrograms.OrderBy(e => e.Description).ToList();
                    //return View(Ordered);
                    break;

                case "muscleGroup_desc":
                    Ordered = WorkoutPrograms.OrderByDescending(e => e.Description).ToList();
                    //return View(Ordered);
                    break;

                case "workouts":
                    Ordered = WorkoutPrograms.OrderBy(e => e.Workouts.Count()).ToList();
                    //return View(Ordered);
                    break;

                case "workouts_desc":
                    Ordered = WorkoutPrograms.OrderByDescending(e => e.Workouts.Count()).ToList();
                    //return View(Ordered);
                    break;

                default:
                    Ordered = WorkoutPrograms.OrderBy(e => e.Name).ToList();
                    //return View(Ordered);
                    break;
            }

            int pageSize = 10;

            var paginatedList = await PaginatedList<WorkoutProgram>.CreateAsync(Ordered, pageNumber ?? 1, pageSize);

            return View(paginatedList);

        }

        // GET: WorkoutPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = _repository.GetById(id);
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
                _repository.Add(workoutProgram);
                _repository.SaveChanges();
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

            var workoutProgram = _repository.GetById(id);
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
                    _repository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutProgramExists(workoutProgram.Id))
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

            var workoutProgram = _repository.GetById(id);
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
            var workoutProgram = _repository.GetById(id);
            _repository.Remove(workoutProgram);
            _repository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutProgramExists(int id)
        {
            return _repository.GetAll().Any(e => e.Id == id);
        }
    }
}

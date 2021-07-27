using GetFit.Domain.Models;
using GetFit.Infrastructure.Repositories;
using GetFit.Infrastructure.SearchSortFilter;
using GetFit.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetFit.Web.Controllers
{
    public class WorkoutProgramsController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<WorkoutProgram> WorkoutPrograms { get; set; }
        public IEnumerable<Workout> Workouts { get; set; }
        public IEnumerable<Excercise> Excercises { get; set; }

        public WorkoutProgramsController(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;
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

            if (!string.IsNullOrEmpty(searchString))
            {
                WorkoutPrograms = await _unitOfWork.WorkoutProgramRepository.Find
                    (wp => wp.Name.Contains(searchString)
                    || wp.Description.Contains(searchString));
            }
            else
            {
                WorkoutPrograms = await _unitOfWork.WorkoutProgramRepository.GetAll();
            }

            IEnumerable<WorkoutProgram> newList = sortOrder switch
            {
                "name_desc" => WorkoutPrograms.OrderByDescending(e => e.Name),
                "muscleGroup" => WorkoutPrograms.OrderBy(e => e.Description),
                "muscleGroup_desc" => WorkoutPrograms.OrderByDescending(e => e.Description),
                _ => WorkoutPrograms.OrderBy(e => e.Name),
            };
            int pageSize = 30;
            try
            {
                return View(PaginatedList<WorkoutProgram>.Create(newList, pageNumber ?? 1, pageSize));
            }
            catch (System.Exception e)
            {
                return View(nameof(Index));
            }

        }

        public async Task<IActionResult> AddWorkoutsToProgramAsync(WorkoutProgram workoutProgram, string sortOrder, string searchString, string currentFilter, int? pageNumber, int? objectId)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MuscleGroupSortParm"] = sortOrder == "muscleGroup" ? "muscleGroup_desc" : "muscleGroup";
            ViewData["CurrentSort"] = sortOrder;

            var currentWorkoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(workoutProgram.Id);
            if (currentWorkoutProgram == null)
            {
                currentWorkoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(objectId);
            }

            if (objectId == null)
            {
                ViewData["ObjectId"] = workoutProgram.Id;
            }
            else
            {
                ViewData["ObjectId"] = objectId;
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;


            if (!string.IsNullOrEmpty(searchString))
            {
                Workouts = await _unitOfWork.WorkoutRepository.Find(
                    w => w.Name.Contains(searchString) ||
                    w.Description.Contains(searchString));
            }
            else
            {
                Workouts = await _unitOfWork.WorkoutRepository.GetAll();
            }

            IEnumerable<Workout> newList = sortOrder switch
            {
                "name_desc" => Workouts.OrderByDescending(e => e.Name),
                "muscleGroup" => Workouts.OrderBy(e => e.Description),
                "muscleGroup_desc" => Workouts.OrderByDescending(e => e.Description),
                _ => Workouts.OrderBy(e => e.Name),
            };
            int pageSize = 30;


            var paginatedList = PaginatedList<Workout>.Create(newList, pageNumber ?? 1, pageSize);


            return View(new AddWorkoutToProgramViewModel() { PaginatedList = paginatedList, WorkoutProgram = currentWorkoutProgram });
        }


        public async Task<IActionResult> AddWorkout(int? workoutId, int? workoutProgramId)
        {
            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(workoutProgramId);
            var workout = await _unitOfWork.WorkoutRepository.GetById(workoutId);

            workoutProgram.Workouts.Add(workout);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("AddWorkoutsToProgram", workoutProgram);

        }

        public async Task<IActionResult> RemoveWorkout(int? workoutId, int? workoutProgramId)
        {
            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(workoutProgramId);
            var workout = await _unitOfWork.WorkoutRepository.GetById(workoutId);

            workoutProgram.Workouts.Remove(workout);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("AddWorkoutsToProgram", workoutProgram);
        }


        // GET: WorkoutPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(id);
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
                await _unitOfWork.WorkoutProgramRepository.Add(workoutProgram);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction("AddWorkoutsToProgram", workoutProgram);
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

            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(id);
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
                    _unitOfWork.WorkoutProgramRepository.EditAsync(workoutProgram);
                    await _unitOfWork.SaveChangesAsync();
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

            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(id);
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
            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetById(id);
            _unitOfWork.WorkoutProgramRepository.Remove(workoutProgram);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkoutProgramExistsAsync(int id)
        {
            var workoutProgram = await _unitOfWork.WorkoutProgramRepository.GetAll();

            return workoutProgram.Any(e => e.Id == id);
        }
    }
}

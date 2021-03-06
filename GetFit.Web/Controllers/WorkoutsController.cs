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
    public class WorkoutsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public IEnumerable<Workout> Workouts { get; set; }
        public IEnumerable<Excercise> Excercises { get; set; }

        public WorkoutsController(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;
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

            var workout = await _unitOfWork.WorkoutRepository.GetById(id);
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
                await _unitOfWork.WorkoutRepository.Add(workout);
                await _unitOfWork.WorkoutRepository.SaveChanges();

                return RedirectToAction("AddExcercisesToWorkout", workout);
            }
            return View(workout);
        }        
       
        public async Task<IActionResult> AddExcercisesToWorkoutAsync(Workout workout, string sortOrder, string searchString, string currentFilter, int? pageNumber, int? objectId)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["MuscleGroupSortParm"] = sortOrder == "muscleGroup" ? "muscleGroup_desc" : "muscleGroup";
            ViewData["CurrentSort"] = sortOrder;

            var currentWorkout = await _unitOfWork.WorkoutRepository.GetById(workout.Id);
            if (currentWorkout == null)
            {
                currentWorkout = await _unitOfWork.WorkoutRepository.GetById(objectId);
            }

            if (objectId == null)
            {
                ViewData["ObjectId"] = workout.Id;
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
                Excercises = await _unitOfWork.ExcerciseRepository.Find(
                    w => w.Name.Contains(searchString) ||
                    w.Description.Contains(searchString));
            }
            else
            {
                Excercises = await _unitOfWork.ExcerciseRepository.GetAll();
            }

            IEnumerable<Excercise> newList = sortOrder switch
            {
                "name_desc" => Excercises.OrderByDescending(e => e.Name),
                "muscleGroup" => Excercises.OrderBy(e => e.Description),
                "muscleGroup_desc" => Excercises.OrderByDescending(e => e.Description),
                _ => Excercises.OrderBy(e => e.Name),
            };
            int pageSize = 30;         


            var paginatedList =  PaginatedList<Excercise>.Create(newList, pageNumber ?? 1, pageSize);


            return View(new AddExcerciseToWorkoutViewModel() {PaginatedList = paginatedList, Workout = currentWorkout });
        }

                
        public async Task<IActionResult> AddExcercise(int? workoutId, int? excerciseId)
        {
            var workout = await _unitOfWork.WorkoutRepository.GetById(workoutId);
            var excercise = await _unitOfWork.ExcerciseRepository.GetById(excerciseId);

            workout.Excercises.Add(excercise);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("AddExcercisesToWorkout", workout);

        }

        public async Task<IActionResult> RemoveExcercise(int? workoutId, int? excerciseId)
        {
            var workout = await _unitOfWork.WorkoutRepository.GetById(workoutId);
            var excercise = await _unitOfWork.ExcerciseRepository.GetById(excerciseId);

            workout.Excercises.Remove(excercise);

            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("AddExcercisesToWorkout", workout);
        }



        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workout = await _unitOfWork.WorkoutRepository.GetById(id);
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
                    _unitOfWork.WorkoutRepository.EditAsync(workout);
                    await _unitOfWork.SaveChangesAsync();
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

            var workout = await _unitOfWork.WorkoutRepository.GetById(id);
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
            var workout = await _unitOfWork.WorkoutRepository.GetById(id);
            _unitOfWork.WorkoutRepository.Remove(workout);
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WorkoutExistsAsync(int id)
        {
            var workout = await _unitOfWork.WorkoutRepository.GetAll();

            return workout.Any(w => w.Id == id);
        }
    }
}

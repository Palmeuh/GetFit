using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GetFit.Domain.Models;
using GetFit.Infrastructure;
using GetFit.Infrastructure.Repositories;
using System.Collections.Generic;
using GetFit.Infrastructure.SearchSortFilter;
using GetFit.Web.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace GetFit.Web.Controllers
{
    public class ExcercisesController : Controller
    {
        private readonly IRepository<Excercise> _repository;
        private readonly GetFitContext _getFitContext;

        public List<Excercise> Ordered { get; set; }
        public IEnumerable<Excercise> Excercises { get; set; }

        public ExcercisesController(IRepository<Excercise> repository, GetFitContext getFitContext)
        {
            _repository = repository;
            _getFitContext = getFitContext;
        }

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

            var excercises =  _repository.GetAllAsQuery();

            if (!string.IsNullOrEmpty(searchString))
            {
                excercises = excercises
                    .Where(w => w.Name.ToUpper().Contains(searchString.ToUpper())
                             || w.Description.Contains(searchString))
                    .Distinct();
            }

            IOrderedQueryable<Excercise> newList = sortOrder switch
            {
                "name_desc" => excercises.OrderByDescending(e => e.Name),
                "muscleGroup" => excercises.OrderBy(e => e.Category),
                "muscleGroup_desc" => excercises.OrderByDescending(e => e.Category),
                _ => excercises.OrderBy(e => e.Name),
            };
            int pageSize = 30;
            try            
            {
                return View(await PaginatedList<Excercise>.CreateAsync(newList.AsNoTracking(), pageNumber ?? 1, pageSize));

            }
            catch (System.Exception e)
            {
                return View(nameof(Index));               
            }

        }

        // GET: Excercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = await _repository.GetById(id);

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
        public async Task<IActionResult> Create([Bind("Id,Name,MuscleGroup,Description, Category")] Excercise excercise)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(excercise);

                await _repository.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(excercise);
        }

        // GET: Excercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = await _repository.GetById(id);
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


            if (id != excercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Edit(excercise);
                    await _repository.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ExcerciseExistsAsync(excercise.Id))
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
            return View(excercise);
        }

        // GET: Excercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise = await _repository.GetById(id);
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
            var excercise = await _repository.GetById(id);

            _repository.Remove(excercise);
            await _repository.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExcerciseExistsAsync(int id)
        {
            var excercises = await _repository.GetAll();            

            return excercises.Any(e => e.Id == id);
        }
    }
}

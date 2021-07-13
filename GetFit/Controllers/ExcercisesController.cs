using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GetFit.Domain.Models;
using GetFit.Infrastructure;
using GetFit.Infrastructure.Repositories;

namespace GetFit.Controllers
{
    public class ExcercisesController : Controller
    {
        private readonly GetFitContext _context;
        private readonly IRepository<Excercise> _repository;

        public ExcercisesController(GetFitContext context, IRepository<Excercise> repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Excercises
        public async Task<IActionResult> Index()
        {
            return View(_repository.GetAll());
        }

        // GET: Excercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excercise =  _repository.GetById(id);
                
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
            Excercise edit = _repository.GetById(id);
            edit.Name = excercise.Name;
            edit.MuscleGroup = excercise.MuscleGroup;
            edit.Description = excercise.Description;

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
            return _context.Excercise.Any(e => e.Id == id);
        }
    }
}

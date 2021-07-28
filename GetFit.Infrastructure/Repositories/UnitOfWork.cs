using GetFit.Domain.Models;
using System.Threading.Tasks;

namespace GetFit.Infrastructure.Repositories
{

    public interface IUnitOfWork
    {
        IRepository<Excercise> ExcerciseRepository { get; }
        IRepository<Workout> WorkoutRepository { get; }
        IRepository<WorkoutProgram> WorkoutProgramRepository { get; }

        Task SaveChangesAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly GetFitContext _context;

        public UnitOfWork(GetFitContext context)
        {
            _context = context;
        }



        private IRepository<Excercise> _excerciseRepository;
        public IRepository<Excercise> ExcerciseRepository
        {
            get
            {
                if (_excerciseRepository == null)
                {
                    _excerciseRepository = new ExcerciseRepository(_context);
                }
                return _excerciseRepository;
            }
        }

        private IRepository<Workout> _workoutRepository;
        public IRepository<Workout> WorkoutRepository
        {
            get
            {
                if (_workoutRepository == null)
                {
                    _workoutRepository = new WorkoutRepository(_context);
                }
                return _workoutRepository;
            }
        }

        private IRepository<WorkoutProgram> _workoutProgramRepository;
        public IRepository<WorkoutProgram> WorkoutProgramRepository
        {
            get
            {
                if (_workoutProgramRepository == null)
                {
                    _workoutProgramRepository = new WorkoutProgramRepository(_context);
                }
                return _workoutProgramRepository;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

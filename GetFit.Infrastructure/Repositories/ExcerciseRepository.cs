using GetFit.Domain.Models;
using System.Linq;

namespace GetFit.Infrastructure.Repositories
{
    public class ExcerciseRepository : GenericRepository<Excercise>
    {
        public ExcerciseRepository(GetFitContext context) : base(context)
        {

        }


        public override Excercise EditAsync(Excercise entity)
        {

            var excercise = _context.Excercise
                .FirstOrDefault(e => e.Id == entity.Id);

            excercise.Name = entity.Name;
            excercise.Category = entity.Category;
            excercise.Description = entity.Description;

            return base.EditAsync(excercise);

        }


    }
}

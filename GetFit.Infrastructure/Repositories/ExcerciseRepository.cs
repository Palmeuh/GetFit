﻿using GetFit.Domain.Models;
using System.Linq;

namespace GetFit.Infrastructure.Repositories
{
    public class ExcerciseRepository : GenericRepository<Excercise>
    {
        public ExcerciseRepository(GetFitContext context) : base(context)
        {

        }

        public override Excercise Edit(Excercise entity)
        {

            var excercise = _context.Excercise
                .FirstOrDefault(e => e.Id == entity.Id);

            excercise.Name = entity.Name;
            excercise.MuscleGroup = entity.MuscleGroup;
            excercise.Description = entity.Description;

            return base.Edit(excercise);

        }


    }
}

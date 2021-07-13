using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GetFit.Domain.Models;

namespace GetFit.Infrastructure
{
    public class GetFitContext : DbContext
    {
        public GetFitContext (DbContextOptions<GetFitContext> options)
            : base(options)
        {
        }

        public DbSet<GetFit.Domain.Models.Excercise> Excercise { get; set; }
    }
}

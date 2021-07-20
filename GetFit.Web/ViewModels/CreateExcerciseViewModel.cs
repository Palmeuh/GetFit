using GetFit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetFit.Web.ViewModels
{
    public class CreateExcerciseViewModel
    {
        public Excercise Excercise{ get; set; }
        public Category Category { get; set; }
    }
}

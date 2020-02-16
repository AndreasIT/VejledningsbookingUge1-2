using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vejledningsbooking.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        //En studerende kan være tilknyttet flere Hold.
        public List<Team> TeamList { get; set; }
    }
}

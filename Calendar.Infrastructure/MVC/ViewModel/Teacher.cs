using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Infrastucture.MVC.ViewModel
{
    public class Teacher
    {
        public int TeacherID { get; set; }
        //Et en Teacher kan være tilknyttet flere Hold.
        public List<Team> TeamList { get; set; }
    }
}

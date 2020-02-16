using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vejledningsbooking.Models
{
    public class Calendar
    {
        public int CalendarID { get; set; }
        public string Description { get; set; }
        //En Calendar kan være tilknyttet et eller flere Hold.
        public Team TeamObject { get; set; }
        public List<Team> TeamList { get; set; }
    }
}

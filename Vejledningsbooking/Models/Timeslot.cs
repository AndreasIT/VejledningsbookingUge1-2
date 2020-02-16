using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vejledningsbooking.Models
{
    public class Timeslot
    {
        public int TimeslotID { get; set; }
        public DateTime SlotStartDateTime { get; set; }
        public DateTime SlotEndDateTime { get; set; }
        //Et Timeslot er tilknyttet én Calendar
        public Calendar CalendarObject { get; set; }

        public Timeslot(DateTime start, DateTime end)
        {
            this.SlotStartDateTime = start;
            this.SlotEndDateTime = end;
        }

    }
}

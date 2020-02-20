using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Domain.MVC.ViewModel
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

        public static bool DoesNotOverlap(IEnumerable<Timeslot> timeslots)
        {
            DateTime endPrior = DateTime.MinValue;
            foreach (Timeslot timeslot in timeslots.OrderBy(x => x.SlotStartDateTime))
            {
                if (timeslot.SlotStartDateTime > timeslot.SlotEndDateTime)
                    return false;
                if (timeslot.SlotStartDateTime < endPrior)
                    return false;
                endPrior = timeslot.SlotEndDateTime;
            }
            return true;
        }

    }
}

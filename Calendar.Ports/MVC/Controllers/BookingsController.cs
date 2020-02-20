using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Calendar.Domain.MVC.ViewModel;

namespace Calendar.Ports.MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingsController(BookingContext context)
        {
            _context = context;
        }

        //En Teacher kan oprette en Calendar
        [HttpPost]
        public async Task<ActionResult<Calendar>> PostCalendar(Calendar calendar, int teacherID)
        {
            if (await _context.Teachers.AnyAsync(x => x.TeacherID == teacherID))
            {
                _context.Calendars.Add(calendar);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetCalendar", new { id = calendar.CalendarID }, calendar);
        }

        //En Teacher kan oprette et Timeslot til en Calendar
        [HttpPost]
        public async Task<ActionResult<Timeslot>> PostTimeslot(Timeslot timeslot, int teacherID, DateTime slotStartDateTime, DateTime slotEndDateTime)
        {
            if (await _context.Teachers.AnyAsync(x => x.TeacherID == teacherID))
            {
                _context.Timeslots.Add(timeslot);
                await _context.SaveChangesAsync();
            }

            //En Teachers Timeslots må ikke være overlappende (uanset hvilken Calendar et
            //overlappende Timeslot måtte være i)
            if (Timeslot.DoesNotOverlap(_context.Timeslots) == true)
            {
                return CreatedAtAction("GetTimeslot", new { id = timeslot.TimeslotID, calendarObject = timeslot.CalendarObject }, timeslot);
            }
            else
            {
                throw new System.ArgumentException("Timeslots cannot overlap");
            }
        }

        //En student kan oprette en booking
        //Mangler: at det er i Calendar, bookningen bliver oprettet
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking, int studentID)
        {
            if (await _context.Students.AnyAsync(x => x.StudentID == studentID))
            {
                _context.Bookings.Add(booking);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        }
    }
}

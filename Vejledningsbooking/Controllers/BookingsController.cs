using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vejledningsbooking.Models;

namespace Vejledningsbooking.Controllers
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

        // GET: api/Bookings
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        //{
        //    return await _context.Bookings.ToListAsync();
        //}

        // GET: api/Bookings/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Booking>> GetBooking(int id)
        //{
        //    var booking = await _context.Bookings.FindAsync(id);

        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    return booking;
        //}

        //PUT: api/Bookings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBooking(int id, Booking booking)
        //{
        //    if (id != booking.BookingID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(booking).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Bookings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost]
        //public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        //{
        //    _context.Bookings.Add(booking);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBooking", new { id = booking.BookingID }, booking);
        //}

        // DELETE: api/Bookings/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Booking>> DeleteBooking(int id)
        //{
        //    var booking = await _context.Bookings.FindAsync(id);
        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Bookings.Remove(booking);
        //    await _context.SaveChangesAsync();

        //    return booking;
        //}

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingID == id);
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
            if (DoesNotOverlap(_context.Timeslots) == true)
            {
                return CreatedAtAction("GetTimeslot", new { id = timeslot.TimeslotID, calendarObject = timeslot.CalendarObject }, timeslot);
            }
            else
            {
                throw new System.ArgumentException("Timeslots cannot overlap");
            }
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

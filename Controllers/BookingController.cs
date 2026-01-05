using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking booking)
        {
            if (booking == null)
                return BadRequest();

            var created = _bookingService.Create(booking);

            return Ok(created);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, Booking booking)
        {
            if (id <= 0 || booking == null)
                return BadRequest();

            var updated = _bookingService.Update(id, booking);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }
        [HttpPatch("{id}")]
        public IActionResult UpdateCustomer(int id, string customer)
        {
            if (id <= 0 || string.IsNullOrEmpty(customer))
                return BadRequest();

            var success = _bookingService.UpdateCustomer(id, customer);

            if (!success)
                return NotFound();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            if (id <= 0)
                return BadRequest();

            var deleted = _bookingService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }



    }

}

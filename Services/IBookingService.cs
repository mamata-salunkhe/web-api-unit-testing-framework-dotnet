using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi.Services
{
    public interface IBookingService
    {
        Booking Create(Booking booking);
        bool Delete(int id);
        Booking Update(int id, Booking booking);

        bool UpdateCustomer(int id, string customer);   
    }
    public class Booking
    {
        public int Id { get; set; }
        public string Customer { get; set; }
    }
}

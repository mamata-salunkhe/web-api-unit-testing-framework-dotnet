using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi.Services
{
    public interface IPaymentService
    {
        Task<Payment> GetPaymentById(int id);

    }
    public class Payment
    {
       public int Id { get; set; } 
       public decimal Amount { get; set; }
    }
}

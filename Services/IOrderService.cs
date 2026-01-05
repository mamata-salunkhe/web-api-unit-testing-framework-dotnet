using System;
using System.Collections.Generic;
using System.Text;

namespace UserApi.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(int orderId);
    }

    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; }
    }


}

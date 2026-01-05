using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            if (orderId <= 0)
                return BadRequest("Invalid order id");

            var order = await _orderService.GetOrderById(orderId);

            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }

}

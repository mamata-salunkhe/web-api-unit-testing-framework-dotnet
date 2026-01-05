using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            if (id <= 0)
                return BadRequest();

            var payment = await _paymentService.GetPaymentById(id);

            if (payment == null)
                return NotFound();

            return Ok(payment);
        }
    }

}

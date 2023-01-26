using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.Services;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _Payment;
        public PaymentController(IPayment payment)
        {
            _Payment = payment;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var payments = await _Payment.GetPayments();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _Payment.GetPayment(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> AddPayment(Payment payment)
        {
            var newPayment = await _Payment.AddPayment(payment);
            return CreatedAtAction(nameof(GetPayment), new { id = newPayment.Id }, newPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }
            var updatedPayment = await _Payment.UpdatePayment(payment);
            if (updatedPayment == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var deleted = await _Payment.DeletePayment(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

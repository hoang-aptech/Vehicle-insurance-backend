using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;
using vehicle_insurance_backend.DataCtxt;
using vehicle_insurance_backend.models;
using Microsoft.AspNetCore.Authorization;

namespace vehicle_insurance_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly string _vnp_HashSecret = "PTXMZDA3D5UDEB18N594AEN0ZSH00ALV";
        private readonly string _vnp_TmnCode = "XCC3PNU2";
        private readonly string _vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string _vnp_ReturnUrl = "http://localhost:3000/payment-success";

        public InsurancesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceDTO>>> GetInsurances()
        {
            var insurances = await (from insurance in _context.insurances
                                    join insuranceContent in _context.insurancecontents
                                    on insurance.id equals insuranceContent.InsuranceId
                                    join insurancePackage in _context.insurancePackage
                                    on insurance.id equals insurancePackage.InsuranceId
                                    select new InsuranceDTO
                                    {
                                        InsuranceId = insurance.id,
                                        Name = insurance.name,
                                        Description = insurance.description,
                                        Price = insurancePackage.Price,
                                        InsurancePackageId = insurancePackage.Id,
                                    }).Distinct().ToListAsync();
            return Ok(insurances);
        }


        // GET: api/Insurances/5
        [Authorize(Roles = "User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Insurance>> GetInsurance(int id)
        {
            var insurance = await _context.insurances.FindAsync(id);

            if (insurance == null)
            {
                return NotFound();
            }

            return insurance;
        }

        // POST: api/Insurances
        [HttpPost]
        public async Task<ActionResult<Insurance>> PostInsurance(Insurance insurance)
        {
            _context.insurances.Add(insurance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInsurance), new { id = insurance.id }, insurance);
        }

        // PUT: api/Insurances/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurance(int id, Insurance insurance)
        {
            if (id != insurance.id)
            {
                return BadRequest();
            }

            _context.Entry(insurance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Insurances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurance(int id)
        {
            try
            {
                var insurance = await _context.insurances.FindAsync(id);
                if (insurance == null)
                {
                    return NotFound();
                }

                _context.insurances.Remove(insurance);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("foreign key constraint"))
                {
                    return BadRequest(new
                    {
                        message = "Unable to delete the Insurance because this Insurance is associated with other data.",
                        details = "This Insurance is linked to customer insurance and insurance package. Please remove or update the related data before deleting."
                    });
                }

                return StatusCode(500, new { message = "An unexpected error occurred while deleting." });
            }
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<IEnumerable<object>>> GetInsuranceByType(string type)
        {
            if (type != "Car" && type != "Motorbike")
            {
                return BadRequest("Invalid insurance type.");
            }

            var insurances = await _context.insurances
                .Where(i => i.type == type)
                .Join(
                    _context.insurancePackage,
                    insurance => insurance.id,
                    insurancePackage => insurancePackage.InsuranceId,
                    (insurance, insurancePackage) => new
                    {
                        insurance.name,
                        insurance.description,
                        insurancePackage.Price
                    })
                .ToListAsync();

            if (insurances == null || !insurances.Any())
            {
                return NotFound("No insurance packages found for this type.");
            }

            return Ok(insurances);
        }


        [HttpGet("pay/{insurancePackageId}")]
        public IActionResult CreatePaymentUrl(int insurancePackageId)
        {
            var insurancePackage = _context.insurancePackage
                .Include(p => p.Insurance)
                .FirstOrDefault(p => p.Id == insurancePackageId && !p.deleted);

            if (insurancePackage == null)
            {
                return NotFound("Insurance package not found.");
            }

            var price = insurancePackage.Price;

            var vnp = new VnPayLibrary();
            vnp.AddRequestData("vnp_Version", "2.1.0");
            vnp.AddRequestData("vnp_Command", "pay");
            vnp.AddRequestData("vnp_TmnCode", _vnp_TmnCode);
            vnp.AddRequestData("vnp_Amount", ((int)(price * 100000)).ToString());
            vnp.AddRequestData("vnp_CurrCode", "VND");

            string txnRef = $"{insurancePackageId}_{DateTime.Now.Ticks}";
            vnp.AddRequestData("vnp_TxnRef", txnRef);

            vnp.AddRequestData("vnp_OrderInfo", $"Payment for insurance package #{insurancePackageId}");
            vnp.AddRequestData("vnp_OrderType", "other");
            vnp.AddRequestData("vnp_Locale", "vn");
            vnp.AddRequestData("vnp_ReturnUrl", _vnp_ReturnUrl);
            vnp.AddRequestData("vnp_IpAddr", vnp.GetIpAddress(HttpContext));
            vnp.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));

            var paymentUrl = vnp.CreateRequestUrl(_vnp_Url, _vnp_HashSecret);
            return Ok(new { paymentUrl });
        }
        [HttpGet("payment-success")]
        public IActionResult PaymentSuccess(string vnp_TxnRef, string vnp_ResponseCode, string vnp_OrderInfo)
        {
            if (string.IsNullOrEmpty(vnp_TxnRef) || string.IsNullOrEmpty(vnp_ResponseCode) || string.IsNullOrEmpty(vnp_OrderInfo))
            {
                return BadRequest(new { message = "Transaction Reference, Response Code, and Order Info are required." });
            }
            if (vnp_ResponseCode == "00")
            {
                var orderInfoParts = vnp_OrderInfo.Split('#');
                if (orderInfoParts.Length < 2 || !int.TryParse(orderInfoParts[1], out int insurancePackageId))
                {
                    return BadRequest(new { message = "Invalid insurance package ID in order info." });
                }
                var insurancePackage = _context.insurancePackage
                    .FirstOrDefault(p => p.Id == insurancePackageId && !p.deleted);

                if (insurancePackage == null)
                {
                    return NotFound("Insurance package not found.");
                }

                var existingBilling = _context.billings
                    .FirstOrDefault(b => b.InsurancePackageId == insurancePackageId
                                         && b.price == insurancePackage.Price
                                         && !b.deleted);
                if (existingBilling != null)
                {
                    return Ok(new { message = "Payment successful, but billing record already exists." });
                }

                // Set the billing information
                var billing = new Billing
                {
                    price = insurancePackage.Price,
                    startDate = DateTime.Today,
                    expireDate = DateTime.Today.AddMonths(insurancePackage.Duration),
                    vehicleId = 1,
                    InsurancePackageId = insurancePackageId,
                    deleted = false,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };

                _context.billings.Add(billing);
                _context.SaveChanges();

                return Ok(new { message = "Payment successful and billing record created." });
            }
            else if (vnp_ResponseCode == "24")
            {
                return Ok(new { message = "Payment canceled by the user." });
            }
            else
            {
                return BadRequest(new { message = "Payment failed." });
            }
        }


        private bool InsuranceExists(int id)
        {
            return _context.insurances.Any(e => e.id == id);
        }
    }
}

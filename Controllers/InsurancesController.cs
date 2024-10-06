using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_insurance_backend.DataCtxt;
using vehicle_insurance_backend.models;

namespace vehicle_insurance_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly DataContext _context;

        public InsurancesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Insurances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insurance>>> Getinsurances()
        {
            return await _context.insurances.ToListAsync();
        }

        // GET: api/Insurances/5
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

        // PUT: api/Insurances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Insurances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Insurance>> PostInsurance(Insurance insurance)
        {
            _context.insurances.Add(insurance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsurance", new { id = insurance.id }, insurance);
        }

        // DELETE: api/Insurances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurance(int id)
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

        private bool InsuranceExists(int id)
        {
            return _context.insurances.Any(e => e.id == id);
        }

        [HttpGet("pay/{insuranceId}")]
        public IActionResult CreatePaymentUrl(int insuranceId)
        {
            // Lấy thông tin bảo hiểm từ cơ sở dữ liệu
            var insurance = _context.insurances.Find(insuranceId);
            if (insurance == null)
            {
                return NotFound("Insurance not found.");
            }

            string vnp_TmnCode = "XCC3PNU2";
            string vnp_HashSecret = "PTXMZDA3D5UDEB18N594AEN0ZSH00ALV";
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_ReturnUrl = "https://bda7-42-114-34-40.ngrok-free.app/payment-success";
            string vnp_Version = "2.1.0";
            string vnp_Command = "pay";
            string vnp_TxnRef = insuranceId.ToString();
            string vnp_OrderInfo = $"Payment for insurance #{insuranceId}";
            string vnp_Amount = (insurance.price * 100).ToString("F0");
            string vnp_CurrCode = "VND";
            string vnp_IpAddr = "127.0.0.1";
            string vnp_CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            string vnp_Locale = "vn";
            string vnp_OrderType = "100000";
            string vnp_ExpireDate = DateTime.Now.AddHours(2).ToString("yyyyMMddHHmmss");

            // Tạo chuỗi truy vấn
            var queryData = new SortedList<string, string>
    {
        { "vnp_Amount", vnp_Amount },
        { "vnp_Command", vnp_Command },
        { "vnp_CreateDate", vnp_CreateDate },
        { "vnp_CurrCode", vnp_CurrCode },
        { "vnp_ExpireDate", vnp_ExpireDate },
        { "vnp_IpAddr", vnp_IpAddr },
        { "vnp_Locale", vnp_Locale },
        { "vnp_OrderInfo", vnp_OrderInfo },
        { "vnp_OrderType", vnp_OrderType },
        { "vnp_ReturnUrl", vnp_ReturnUrl },
        { "vnp_TmnCode", vnp_TmnCode },
        { "vnp_TxnRef", vnp_TxnRef },
        { "vnp_Version", vnp_Version },
        //{"vnp_SecureHashType" , "SHA256" }
        {"vnp_SecureHashType" , "HMACSHA512" }
    };

            // Thêm mã xác thực
            //string queryString = BuildQueryString(queryData);
            //string vnp_SecureHash = HmacSHA512(vnp_HashSecret, queryString);
            string queryString = BuildQueryString(queryData);
            string vnp_SecureHash = HmacSHA512(vnp_HashSecret,queryString);
            //string vnp_SecureHash = ComputeSHA256(queryString);
            string paymentUrl = $"{vnp_Url}?{queryString}&vnp_SecureHash={vnp_SecureHash}";
            Console.WriteLine(paymentUrl);
            // Ghi log cho việc gỡ lỗi
            Console.WriteLine($"Query String: {queryString}");
            Console.WriteLine($"Secure Hash: {vnp_SecureHash}");

            return Ok(new { paymentUrl });
        }

        // Tạo chuỗi truy vấn
        private string BuildQueryString(SortedList<string, string> data)
        {
            var queryString = new StringBuilder();
            foreach (var kvp in data)
            {
                queryString.Append(HttpUtility.UrlEncode(kvp.Key) + "=" + HttpUtility.UrlEncode(kvp.Value) + "&");
            }
            queryString.Length--; // Xóa ký tự "&" cuối cùng
            return queryString.ToString();
        }

        // Mã hóa chuỗi truy vấn với HMAC SHA512
        private static string HmacSHA512(string key, string data)
        {
            var hash = new StringBuilder();
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                var hashBytes = hmac.ComputeHash(dataBytes);
                foreach (var b in hashBytes)
                {
                    hash.Append(b.ToString("x2"));
                }
            }
            return hash.ToString();
        }
        //public string Sha256(string input)
        //{
        //    using (SHA256 sha256Hash = SHA256.Create())
        //    {
        //        // Chuyển đổi chuỗi thành mảng byte
        //        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        //        // Chuyển đổi mảng byte thành chuỗi hexa
        //        StringBuilder builder = new StringBuilder();
        //        foreach (byte b in bytes)
        //        {
        //            builder.Append(b.ToString("x2"));
        //        }
        //        return builder.ToString();
        //    }
        //}

        private string ComputeSHA256(string input)
        {
            try
            {

                byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                String str = builder.ToString();

                return str;
                //return "";

            }
            catch (Exception ex)
            {
                // Log the exception or print to console for debugging
                Console.WriteLine($"Error in ComputeSHA256: {ex.Message}");
                throw new Exception("An error occurred while computing the hash."); // You can customize the error message for debugging
               
            }

            return "";
        }
    }
}

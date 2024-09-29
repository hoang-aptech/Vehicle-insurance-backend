using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class Message
    {
        [Key]
        public int id {  get; set; }

        [Required]
        public DateTime time { get; set; }
        [Required, StringLength(255)]
        public string message { get; set; }

        [Required]
        public int customersupportId { get; set; }
        public CustomerSupport? CustomerSupport { get; set; }

        public string role { get; set; }

        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        public Message()
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            createdAt = now;
            updatedAt = now;
        }
    }
}

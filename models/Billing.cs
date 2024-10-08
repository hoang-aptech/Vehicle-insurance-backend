using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("Billing")]
    public class Billing
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal price { get; set; }

        [Required]
        public DateTime startDate { get; set; }

        [Required]
        public DateTime expireDate { get; set; }

        [Required]
        public int vehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }
        [Required]
        public int InsurancePackageId { get; set; }
        public InsurancePackage? InsurancePackage { get; set; }

        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        public Billing()
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            createdAt = now;
            updatedAt = now;
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class InsurancePackage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        public int InsuranceId { get; set; }

        public Insurance? Insurance { get; set; }

        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        public InsurancePackage()
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            createdAt = now;
            updatedAt = now;
        }
    }
}

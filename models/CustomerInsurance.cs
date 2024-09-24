using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("CustomerInsurance")]
    public class CustomerInsurance
    {
        [Key]
        public int id { get; set; }

        [Required]
        public DateTime startDate { get; set; }

        [Required]
        public DateTime expireDate { get; set; }

        [Required]
        public Status deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        public Vehicle? vehicle { get; set; }
        [Required]
        [ForeignKey("Vehicle")]
        public int vehicleId { get; set; }
        public Insurance? insurance { get; set; }
        [Required]
        [ForeignKey("Insurance")]
        public int insuranceId { get; set; }
    }

}

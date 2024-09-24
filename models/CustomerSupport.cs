using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("CustomerSupport")]
    public class CustomerSupport
    {
        [Key]
        public int id { get; set; }

        [Required]
        public SupportType type { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(255)]
        public string place { get; set; }

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
    }

    public enum SupportType
    {
        Maintenance,
        Repair,
        TechnicalIssue,
        Inquiry,
        Other
    }
}

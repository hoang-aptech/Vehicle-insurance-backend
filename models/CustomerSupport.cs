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
        public string type { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(255)]
        public string place { get; set; }

        [Required]
        public int vehicleId { get; set; }
        public Vehicle? vehicle { get; set; }

        [Required]
        public int userId { get; set; }
        public User? User { get; set; }

        public string status { get; set; }

        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

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

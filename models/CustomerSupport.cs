using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class CustomerSupport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public SupportType Type { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Place { get; set; }

        [Required]
        public SupportStatus Deleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public Vehicle? Vehicle { get; set; }
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
    }

    public enum SupportType
    {
        Maintenance,
        Repair,
        TechnicalIssue,
        Inquiry,
        Other
    }

    public enum SupportStatus
    {
        Yes,
        No
    }
}

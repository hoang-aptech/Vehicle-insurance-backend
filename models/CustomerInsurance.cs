using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class CustomerInsurance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        public InsuranceStatus Deleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public Vehicle? Vehicle { get; set; }
        [Required]
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public Insurance? Insurance { get; set; }
        [Required]
        [ForeignKey("Insurance")]
        public int InsuranceId { get; set; }
    }

    public enum InsuranceStatus
    {
        Yes,
        No
    }

}

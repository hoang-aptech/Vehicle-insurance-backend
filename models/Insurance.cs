using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class Insurance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public InsuranceName Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        [Required]
        public InsuranceStatus Deleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }

    public enum InsuranceName
    {
        Car,
        Motorbike
    }
}

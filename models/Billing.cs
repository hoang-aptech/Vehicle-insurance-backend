using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }

        [Required]
        public BillingStatus Deleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public User? User { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
    }

    public enum BillingStatus
    {
        Yes,
        No
    }
}

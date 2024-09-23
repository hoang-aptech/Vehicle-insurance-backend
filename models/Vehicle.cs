using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [StringLength(50)]
        public string Version { get; set; }

        [Required]
        public VehicleType Type { get; set; }

        [Required]
        [StringLength(20)]
        public string CarNumber { get; set; }

        [Required]
        public VehicleStatus Deleted { get; set; }

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

    public enum VehicleType
    {
        Car,
        Motorbike
    }

    public enum VehicleStatus
    {
        Yes,
        No
    }

}

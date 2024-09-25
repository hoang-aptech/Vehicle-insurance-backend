using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string model { get; set; }

        [Required]
        [StringLength(50)]
        public string version { get; set; }

        [Required]
        public VehicleType type { get; set; }

        [Required]
        [StringLength(20)]
        public string carNumber { get; set; }

        [Required]
        public Status deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }
        public User? User { get; set; }
        [Required]
        [ForeignKey("User")]
        public int userId { get; set; }
    }

    public enum VehicleType
    {
        Car,
        Motorbike
    }


}

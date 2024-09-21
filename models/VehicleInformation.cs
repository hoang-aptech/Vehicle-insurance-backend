using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class VehicleInformation
    {
        [Key]
        public int VehicleId { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public string VehiclesOwnerName { get; set; }
        [Required]
        public string VehicleModel { get; set; }
        [Required]
        public string VehicleVersion { get; set; }
        [Required]
        public int VehicleRate { get; set; }
        [Required]
        public string VehicleBodyNumber { get; set; }
        [Required]
        public string VehicleEngineNumber { get; set; }
        [Required]
        public int VehicleNumber { get; set; }
    }
}

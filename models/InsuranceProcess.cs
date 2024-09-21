using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class InsuranceProcess
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustommerId { get; set; }
        public CustomerInformation? CustomerInformation { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAdd { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public int PolicyNumber { get; set; }
        [Required]
        public string PolicyDate { get; set; }
        [Required]
        public int PolicyDuration { get; set; }
        [Required]
        public int VehicleNumber { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public string VehicleModel { get; set; }
        [Required]
        public string VehicleVersion { get; set; }
        [Required]
        public int VehicleRate { get; set; }
        [Required]
        public string VehicleWarranty { get; set; }
        [Required]
        public string VehicleBodyNumber { get; set; }
        [Required]
        public string VehicleEngineNumber { get; set; }
        [Required]
        public string CustomerAddProve { get; set; }
    }
}

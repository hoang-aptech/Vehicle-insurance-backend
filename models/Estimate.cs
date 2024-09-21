using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class Estimate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public CustomerInformation? Customer { get; set; }
        [Required]
        public int EstimateNumber { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public string VehicleModel { get; set; }
        [Required]
        public int VehicleRate { get; set; }
        [Required]
        public string VehicleWarranty { get; set; }
        [Required]
        public string VehiclePolicyType { get; set; }
    }
}

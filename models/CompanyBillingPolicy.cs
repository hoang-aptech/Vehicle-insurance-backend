using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class CompanyBillingPolicy
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public CustomerInformation? CustomerInformation { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public int PolicyNumber { get; set; }
        [Required]
        public string CustomerAddProve { get; set; }
        [Required]
        public int CustomerPhoneNumber { get; set; }
        [Required]
        public int BillNo { get; set; }
        [Required]
        public string VehicleName { get; set; }
        [Required]
        public string VehicleModel { get; set; }
        [Required]
        public int VehicleRate { get; set; }
        [Required]
        public string VehicleBodyNumber { get; set; }
        [Required]
        public string VehicleEngineNumber { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}

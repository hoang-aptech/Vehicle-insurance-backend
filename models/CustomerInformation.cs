using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class CustomerInformation
    {
        [Key]
        public int CustomerId { get; set; }
        [Required, StringLength(255, MinimumLength = 3)]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerAdd { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
    }
}

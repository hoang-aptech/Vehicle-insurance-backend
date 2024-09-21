using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class ClaimDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ClaimNumber { get; set; }
        [Required]
        public int PolicyNumber { get; set; }
        [Required]
        public string PolicyStartDate { get; set; }
        [Required]
        public string PolicyEndDate { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string PlaceOfAccident { get; set; }
        [Required]
        public string DateOfAccident { get; set; }
        [Required]
        public int InsuredAmount { get; set; }
        [Required]
        public int ClaimableAmount { get; set; }
    }
}

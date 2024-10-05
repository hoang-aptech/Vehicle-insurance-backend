using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class Insurancecontent
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int InsuranceId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string ContentType { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("Billing")]
    public class Billing
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public double price { get; set; }

        [Required]
        public int customerinsuranceId { get; set; }
        public CustomerInsurance? CustomerInsurance { get; set; }


        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

    }
}

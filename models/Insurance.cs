using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("Insurance")]
    public class Insurance
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [Required]
        public int duration { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal price { get; set; }

        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("New")]
    public class New
    {
        [Key]
        public int id { get; set; }
        [StringLength(100)]
        [Required]
        public string name { get; set; }

        [StringLength(255)]
        [Required]
        public string description { get; set; }

        [StringLength(100)]
        public string image_path { get; set; }
    }
}

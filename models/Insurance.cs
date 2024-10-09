using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace vehicle_insurance_backend.models
{
    [Table("Insurance")]
    public class Insurance
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public string description { get; set; }

        public string clause { get; set; }

        [Required]
        public Boolean isNew { get; set; }

        [Required]
        [StringLength(50)]
        public string type { get; set; }

        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        [JsonIgnore]
        public ICollection<InsurancePackage> InsurancePackages { get; set; }

        public Insurance()
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            createdAt = now;
            updatedAt = now;
        }
    }
}

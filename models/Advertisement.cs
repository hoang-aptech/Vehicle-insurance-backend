﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    [Table("Advertisement")]
    public class Advertisement
    {
        [Key]
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string customerName { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string customerPhone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string customerEmail { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public Boolean deleted { get; set; }

        public DateTime? deletedAt { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        public DateTime updatedAt { get; set; }

        public Advertisement()
        {
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));

            createdAt = now;
            updatedAt = now;
        }
    }

}

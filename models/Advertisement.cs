using System;
using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string CustomerPhone { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        [Required]
        public AdvertisementType Type { get; set; }

        [Required]
        public AdvertisementStatus Deleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }

    public enum AdvertisementType
    {
        Car,
        Motorbike
    }

    public enum AdvertisementStatus
    {
        Yes,
        No
    }

}

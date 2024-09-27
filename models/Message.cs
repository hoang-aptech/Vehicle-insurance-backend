using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vehicle_insurance_backend.models
{
    public class Message
    {
        [Key]
        public int id {  get; set; }

        [Required]
        public DateTime time { get; set; }
        [Required]
        public string message { get; set; }

        [Required]
        public int customersupportId { get; set; }
        public CustomerSupport? CustomerSupport { get; set; }

        public Role role { get; set; }

        public enum Role
        {
            Employee,
            User
        }
    }
}

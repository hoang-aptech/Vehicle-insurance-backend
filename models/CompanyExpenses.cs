using System.ComponentModel.DataAnnotations;

namespace vehicle_insurance_backend.models
{
    public class CompanyExpenses
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DateOfExpense { get; set; }
        [Required]
        public string TypeOfExpense { get; set; }
        [Required]
        public int AmountOfExpense { get; set; }
    }
}

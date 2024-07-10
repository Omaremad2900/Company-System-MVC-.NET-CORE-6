using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max Lenght is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min length is 5 Chars")]
        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "Age Must be In range froem 22 to 35")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be like 123-Street-City-Country")]

        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public string Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        [AllowNull]
        public int? DepartmentId { get; set; }
        [InverseProperty("Employees")]
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }


    }
}


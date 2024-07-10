using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
       
        public int? Age { get; set; }
       

        public string Address   { get; set; }
       
        public string Salary { get; set; }
        
        public bool IsActive { get; set; }

        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; }= DateTime.Now;
        [AllowNull]
        public string? ImageName { get; set; }
        [AllowNull]
        public int? DepartmentId { get; set; } 
        [InverseProperty("Employees")]
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
       

    }
}

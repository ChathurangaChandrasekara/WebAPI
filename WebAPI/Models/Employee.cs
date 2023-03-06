using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; }
        public DateTime DOJ { get; set; }
    }
}

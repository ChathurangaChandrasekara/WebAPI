namespace WebAPI.Models
{
    public class EmployeeDetailsDTO
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
        public DateTime DOJ { get; set; }
    }
}

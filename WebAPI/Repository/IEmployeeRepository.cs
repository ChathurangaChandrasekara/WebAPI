using WebAPI.Models;

namespace WebAPI.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDetailsDTO>> GetEmployees();
        Task<EmployeeDetailsDTO> GetEmployeeByID(int ID);
        Task<Employee> InsertEmployee(Employee objEmployee);
        Task<Employee> UpdateEmployee(Employee objEmployee);
        bool DeleteEmployee(int ID);
    }
}

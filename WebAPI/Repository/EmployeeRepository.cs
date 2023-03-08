using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly APIDbContext _appDBContext;
        public EmployeeRepository(APIDbContext context)
        {
            _appDBContext = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<EmployeeDetailsDTO>> GetEmployees()
        {
            var data = await (from emp in _appDBContext.Employees
                        join dep in _appDBContext.Departments
                        on emp.DepartmentId equals dep.DepartmentId
                        select new EmployeeDetailsDTO
                        {
                            EmployeeID = emp.EmployeeID,
                            EmployeeName = emp.EmployeeName,
                            Email = emp.Email,
                            DOJ = emp.DOJ,
                            DepartmentId = emp.DepartmentId,
                            DepartmentName = dep.DepartmentName
                        }).ToListAsync();
            return data;
        }
        public async Task<EmployeeDetailsDTO> GetEmployeeByID(int ID)
        {
            var data =  await (from emp in _appDBContext.Employees
                              join dep in _appDBContext.Departments
                              on emp.DepartmentId equals dep.DepartmentId
                              where emp.EmployeeID == ID
                              select new EmployeeDetailsDTO
                              {
                                  EmployeeID = emp.EmployeeID,
                                  EmployeeName = emp.EmployeeName,
                                  Email = emp.Email,
                                  DOJ = emp.DOJ,
                                  DepartmentId = emp.DepartmentId,
                                  DepartmentName = dep.DepartmentName
                              }).SingleOrDefaultAsync();
            return data;
        }
        public async Task<Employee> InsertEmployee(Employee objEmployee)
        {
            _appDBContext.Employees.Add(objEmployee);
            await _appDBContext.SaveChangesAsync();
            return objEmployee;
        }
        public async Task<Employee> UpdateEmployee(Employee objEmployee)
        {
            _appDBContext.Entry(objEmployee).State = EntityState.Modified;
            await _appDBContext.SaveChangesAsync();
            return objEmployee;
        }
        public bool DeleteEmployee(int ID)
        {
            bool result = false;
            var department = _appDBContext.Employees.Find(ID);
            if (department != null)
            {
                _appDBContext.Entry(department).State = EntityState.Deleted;
                _appDBContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}

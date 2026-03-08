using hanin.Entities;
using System.Threading.Tasks;

namespace hanin.ServiceIntefrace
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<EmployeeEntity>>> GetAllEmployeesAsync();     
        Task<ServiceResponse<EmployeeEntity>> AddEmployeeAsync(EmployeeEntity employee);
    }
}

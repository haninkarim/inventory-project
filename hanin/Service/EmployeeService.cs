using hanin;
using hanin.DBContext;
using hanin.Entities;
using hanin.ServiceIntefrace;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;

    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<EmployeeEntity>> AddEmployeeAsync(EmployeeEntity employee)
    {
        var response = new ServiceResponse<EmployeeEntity>();
        try
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(); 

            response.Data = employee;
            response.Message = "Employee saved successfully.";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceResponse<List<EmployeeEntity>>> GetAllEmployeesAsync()
    {
        var response = new ServiceResponse<List<EmployeeEntity>>();
        response.Data = await _context.Employees.ToListAsync();
        return response;
    }
}
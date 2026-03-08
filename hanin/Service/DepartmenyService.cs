using hanin;
using hanin.DBContext;
using hanin.Entities;
using Microsoft.EntityFrameworkCore;

public class DepartmentService : IDepartmentService
{
    private readonly AppDbContext _context;

    public DepartmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<DepartmentEntity>> AddDeptAsync(DepartmentEntity dept)
    {
        var response = new ServiceResponse<DepartmentEntity>();
        try
        {
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();

            response.Data = dept;
            response.Message = "Department created successfully.";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Database error: {ex.Message}";
        }
        return response;
    }

    public async Task<ServiceResponse<List<DepartmentEntity>>> GetAllDeptsAsync()
    {
        var response = new ServiceResponse<List<DepartmentEntity>>();
        try
        {
            var data = await _context.Departments.ToListAsync();

            response.Data = data;
            response.Message = "Departments retrieved successfully.";
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Failed to fetch departments: {ex.Message}";
        }
        return response;
    }
}
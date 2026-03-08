using hanin;
using hanin.Entities;

public interface IDepartmentService
{
    Task<ServiceResponse<DepartmentEntity>> AddDeptAsync(DepartmentEntity dept);
    Task<ServiceResponse<List<DepartmentEntity>>> GetAllDeptsAsync();
}
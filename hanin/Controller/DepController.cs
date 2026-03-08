using hanin.Entities;
using hanin.ServiceIntefrace;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DeptController : ControllerBase
{
    private readonly IDepartmentService _deptService;

    public DeptController(IDepartmentService deptService)
    {
        _deptService = deptService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(DepartmentEntity dept)
    {
        var result = await _deptService.AddDeptAsync(dept);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _deptService.GetAllDeptsAsync();
        return Ok(result);
    }
}
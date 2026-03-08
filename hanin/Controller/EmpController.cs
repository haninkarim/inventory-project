using hanin.Entities;
using hanin.ServiceIntefrace;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmpController : ControllerBase
{
    private readonly IEmployeeService _empService;

    public EmpController(IEmployeeService empService)
    {
        _empService = empService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeEntity employee)
    {
        var result = await _empService.AddEmployeeAsync(employee);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _empService.GetAllEmployeesAsync();
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }
}
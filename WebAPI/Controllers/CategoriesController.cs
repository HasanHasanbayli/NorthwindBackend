using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IConfiguration Configuration;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
        Configuration.GetSection("").Get<object>();
    }

    [HttpGet("getall")]
    public IActionResult GetList()
    {
        var result = _categoryService.GetList();

        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.Message);
    }
}
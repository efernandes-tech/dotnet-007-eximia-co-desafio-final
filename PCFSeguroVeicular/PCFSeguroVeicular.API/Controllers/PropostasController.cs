using Microsoft.AspNetCore.Mvc;

namespace PCFSeguroVeicular.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/{controller}")]
[ApiVersion("1.0")]
public class PropostasController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CriarProposta()
    {
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> ConsultarProposta()
    {
        return Ok();
    }

    [HttpPost("Aprovar")]
    public async Task<IActionResult> AprovarProposta()
    {
        return Ok();
    }

    [HttpPost("Reprovar")]
    public async Task<IActionResult> ReprovarProposta()
    {
        return Ok();
    }
}

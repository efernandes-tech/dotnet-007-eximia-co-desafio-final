using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PCFSeguroVeicular.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/{controller}")]
[ApiVersion("1.0")]
public class PropostasController : ControllerBase
{
    public record Residencia(
        string UF,
        string Cidade,
        string Bairro);

    public record Cobertura(
        string Codigo);

    public record NovaPropostaRequest(
        //Veículo: marca, modelo e ano
        string veiculoMarca,
        string veiculoModelo,
        string veiculoAno,
        //Proprietário: cpf, nome, data nascimento, residência
        string proprietarioCpf,
        string proprietarioNome,
        string proprietarioDtNascimento,
        Residencia proprietarioResidencia,
        //Condutor: cpf, data nascimento, residência.
        string condutorCpf,
        string condutorDtNascimento,
        Residencia condutorResidencia,
        //Coberturas: coberturas(roubo/furto, colisão, terceiros, proteção residencial) selecionadas pelo usuário
        Cobertura[] coberturas
        );

    [HttpPost]
    public async Task<IActionResult> CriarProposta(
        [FromBody] NovaPropostaRequest input,
        CancellationToken cancellationToken
        )
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

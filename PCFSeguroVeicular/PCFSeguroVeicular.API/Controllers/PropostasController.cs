using Microsoft.AspNetCore.Mvc;
using PCFSeguroVeicular.API.Domain.Propostas.Features.NovaProposta;

namespace PCFSeguroVeicular.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/{controller}")]
[ApiVersion("1.0")]
public class PropostasController : ControllerBase
{
    public record VeiculoModel(
        string Marca,
        string Modelo,
        string Ano
    );

    public record ResidenciaModel(
        string UF,
        string Cidade,
        string Bairro
    );

    public record ClienteModel(
        string Cpf,
        DateTime DtNascimento,
        ResidenciaModel residencia,
        string? Nome = null
    );

    public record CoberturaModel(
        string Codigo
    );

    public record CriarPropostaRequest(
        //Veículo: marca, modelo e ano
        VeiculoModel veiculo,
        //Proprietário: cpf, nome, data nascimento, residência
        ClienteModel proprietario,
        //Condutor: cpf, data nascimento, residência.
        ClienteModel condutor,
        //Coberturas: coberturas(roubo/furto, colisão, terceiros, proteção residencial) selecionadas pelo usuário
        CoberturaModel[] coberturas
    );

    [HttpPost]
    public async Task<IActionResult> CriarProposta(
        [FromBody] CriarPropostaRequest request,
        [FromServices] NovaPropostaHandler handler,
        CancellationToken cancellationToken
        )
    {
        var command = new NovaPropostaCommand(
            veiculo: new DadosVeiculo(
                request.veiculo.Marca,
                request.veiculo.Modelo,
                request.veiculo.Ano
            ),
            proprietario: new DadosCliente(
                request.proprietario.Cpf,
                request.proprietario.DtNascimento,
                residencia: new DadosResidencia(
                    request.proprietario.residencia.UF,
                    request.proprietario.residencia.Cidade,
                    request.proprietario.residencia.Bairro
                ),
                request.proprietario.Nome
            ),
            condutor: new DadosCliente(
                request.condutor.Cpf,
                request.condutor.DtNascimento,
                residencia: new DadosResidencia(
                    request.condutor.residencia.UF,
                    request.condutor.residencia.Cidade,
                    request.condutor.residencia.Bairro
                )
            ),
            coberturas: request.coberturas.Select(c => new DadosCobertura(c.Codigo)).ToArray()
        );

        var result = await handler.ExecutarAsync(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
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

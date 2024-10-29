using Microsoft.AspNetCore.Mvc;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Aplicacao;
using PlataformaClienteFinal.API.Dominio.Infraestrutura;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PlataformaClienteFinal.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditoConsignadoController : ControllerBase
{
    public record IncluirPropostaRequest(
        // TODO: Aplicar required
        string CpfCliente, string DadosRendimento, string Endereco, string Telefone, string Email,
        decimal ValorDesejado, // TODO: Receber uma simulação (vlr desejado, qtd parcelas, valor da parcela, taxa de juros mensal, valor total = valor operacao)
        int CodigoAgente);

    [HttpGet("Propostas/{id}")]
    public async Task<IActionResult> ConsultarProposta(
    string id,
    CancellationToken cancellationToken)
    {
        // TODO: Buscar com query
        return Ok(id);
    }

    [HttpPost("Propostas")]
    public async Task<IActionResult> IncluirProposta(
        [FromBody] IncluirPropostaRequest request,
        [FromServices] IncluirPropostaHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new IncluirPropostaCommand
        {
            Cliente = request.CpfCliente,
            Simulacao = request.ValorDesejado,
            Agente = request.CodigoAgente
        };

        var result = await handler.Handle(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}

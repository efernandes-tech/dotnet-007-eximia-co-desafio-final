using CSharpFunctionalExtensions;
using System.Text;

namespace PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;

public sealed class Proposta : Entity<Guid>
{
    public Guid Id { get; }
    public string Cliente { get; }
    public decimal ValorOperacao { get; }
    public Agente Agente { get; }
    public EPropostaSituacao Situacao { get; }

    private Proposta()
    { }

    private Proposta(Guid id, string cliente, decimal valorOperacao, Agente agente,
        EPropostaSituacao situacao)
    {
        Id = id;
        Cliente = cliente;
        ValorOperacao = valorOperacao;
        Agente = agente;
        Situacao = situacao;
    }

    public static Result<Proposta> Criar(Cliente cliente, decimal valorOperacao, Agente agente)
    {
        var validacoes = new List<IValidacaoProposta>
        {
            new ValidacaoClienteBloqueado(),
            new ValidacaoAgenteInativo(),
        };

        foreach (var validacao in validacoes)
        {
            var resultado = validacao.Validar(cliente, agente);
            if (resultado.IsFailure)
                return Result.Failure<Proposta>(resultado.Error);
        }

        var proposta = new Proposta(
            Guid.NewGuid(),
            cliente.Cpf,
            valorOperacao,
            agente,
            EPropostaSituacao.EmAnalise);

        return Result.Success(proposta);
    }
}

public enum EPropostaSituacao
{
    EmAnalise = 10,
    Aprovada = 20,
    Reprovada = 30
}

public interface IValidacaoProposta
{
    Result Validar(Cliente cliente, Agente agente);
}

// DESAFIO: Cpf deve estar liberado (não pode estar bloqueado);
public class ValidacaoClienteBloqueado : IValidacaoProposta
{
    public Result Validar(Cliente cliente, Agente agente)
    {
        if (cliente.Bloqueado)
            return Result.Failure("CPF do cliente bloqueado");

        return Result.Success();
    }
}

// DESAFIO: Agente que está incluindo a proposta deve estar ativo;
public class ValidacaoAgenteInativo : IValidacaoProposta
{
    public Result Validar(Cliente cliente, Agente agente)
    {
        if (!agente.Ativo)
            return Result.Failure("Agente inativo");

        return Result.Success();
    }
}

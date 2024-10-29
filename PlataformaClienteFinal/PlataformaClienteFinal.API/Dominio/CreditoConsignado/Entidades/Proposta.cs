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
        // DESAFIO: Cpf deve estar liberado (não pode estar bloqueado);
        if (cliente.Bloqueado)
            return Result.Failure<Proposta>("CPF do cliente bloqueado");

        // DESAFIO: Agente que está incluindo a proposta deve estar ativo;
        if (agente.Ativo)
            return Result.Failure<Proposta>("Agente inativo");

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
{ EmAnalise = 10, Aprovada = 20, Reprovada = 30 }

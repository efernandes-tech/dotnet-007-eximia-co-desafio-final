using CSharpFunctionalExtensions;

namespace PCFSeguroVeicular.API.Domain.Propostas.Entidades;

public sealed class Proposta : Entity<int>
{
    public decimal VlrTotalSeguro { get; }

    private Proposta()
    { }

    private Proposta(
        int id,
        decimal vlrTotalSeguro
    ) : base(id)
    {
        VlrTotalSeguro = vlrTotalSeguro;
    }

    public static Result<Proposta> Criar(
        int id,
        decimal vlrTotalSeguro
        )
    {
        // Regras...

        return Result.Success(new Proposta(id, vlrTotalSeguro));
    }
}

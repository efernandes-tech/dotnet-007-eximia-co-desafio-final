using CSharpFunctionalExtensions;

namespace PCFSeguroVeicular.API.Domain.Coberturas.Entidades;

public class Cobertura : Entity<string>
{
    public string Codigo { get; }
    public string Descricao { get; }
}

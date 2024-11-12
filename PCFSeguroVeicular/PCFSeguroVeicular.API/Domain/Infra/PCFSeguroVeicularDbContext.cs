using Microsoft.EntityFrameworkCore;
using PCFSeguroVeicular.API.Domain.Coberturas.Entidades;
using PCFSeguroVeicular.API.Domain.Infra.Mappings;
using PCFSeguroVeicular.API.Domain.Propostas.Entidades;

namespace PCFSeguroVeicular.API.Domain.Infra;

public class PCFSeguroVeicularDbContext : DbContext
{
    public PCFSeguroVeicularDbContext(DbContextOptions<PCFSeguroVeicularDbContext> options)
        : base(options)
    {
    }

    public DbSet<Proposta> Propostas { get; set; }
    public DbSet<Cobertura> Coberturas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PropostaMap());
        base.OnModelCreating(modelBuilder);
    }
}

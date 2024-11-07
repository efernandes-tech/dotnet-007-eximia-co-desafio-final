using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Infraestrutura.Mapeamento;

namespace PlataformaClienteFinal.API.Dominio;

public class PlataformaClienteFinalDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "PlataformaClienteFinalDB";

    public DbSet<Proposta> PropostaCreditoConsignados { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Agente> Agentes { get; set; }

    public PlataformaClienteFinalDbContext(DbContextOptions<PlataformaClienteFinalDbContext> options)
        : base(options) { }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            // TODO: Aplicar datas de criacao e edicao (e usuario?)
            //foreach (var item in ChangeTracker.Entries())
            //{
            //    if ((item.State == EntityState.Modified || item.State == EntityState.Added)
            //        && item.Properties.Any(c => c.Metadata.Name == "DataUltimaAlteracao"))
            //        item.Property("DataUltimaAlteracao").CurrentValue = DateTime.UtcNow;

            //    if (item.State == EntityState.Added)
            //        if (item.Properties.Any(c => c.Metadata.Name == "DataCadastro") && item.Property("DataCadastro").CurrentValue.GetType() != typeof(DateTime))
            //            item.Property("DataCadastro").CurrentValue = DateTime.UtcNow;
            //}
            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return result;
        }
        catch (DbUpdateException e)
        {
            throw new Exception();
        }
        catch (Exception)
        {
            throw;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AgenteConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new PropostaConfiguration());
    }
}

public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<PlataformaClienteFinalDbContext>
{
    public PlataformaClienteFinalDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PlataformaClienteFinalDbContext>();
        var connectionString = "Server=localhost,1433;Database=PlataformaClienteFinalDB;User Id=sa;Password=SenhaForte123!;TrustServerCertificate=True;";

        optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });

        return new PlataformaClienteFinalDbContext(optionsBuilder.Options);
    }
}

using Microsoft.EntityFrameworkCore;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;

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
        // TODO: Aplicar mapeamento
        //modelBuilder.ApplyConfiguration(new PropostaCreditoConsignadosConfigurations());
    }
}

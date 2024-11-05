using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;

namespace PlataformaClienteFinal.API.Dominio.CreditoConsignado.Infraestrutura.Mapeamento;

public class AgenteConfiguration : IEntityTypeConfiguration<Agente>
{
    public void Configure(EntityTypeBuilder<Agente> builder)
    {
        builder.ToTable("Agente");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Codigo)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Ativo)
            .HasDefaultValue(false)
            .IsRequired();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;

namespace PlataformaClienteFinal.API.Dominio.CreditoConsignado.Infraestrutura.Mapeamento;

public class PropostaConfiguration : IEntityTypeConfiguration<Proposta>
{
    public void Configure(EntityTypeBuilder<Proposta> builder)
    {
        builder.ToTable("Proposta");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Cliente)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(i => i.ValorOperacao)
            .IsRequired()
            .HasColumnType("decimal")
            .HasPrecision(18, 2);

        builder.Property(a => a.Situacao)
            .IsRequired()
            .HasConversion(new EnumToStringConverter<EPropostaSituacao>())
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasOne(i => i.Agente)
            .WithMany()
            .HasForeignKey("AgenteId")
            .IsRequired();
    }
}

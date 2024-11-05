using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        //builder.Property(i => i.Responsavel)
        //    .IsRequired()
        //    .HasMaxLength(100);

        //builder.Property(i => i.Ativa)
        //    .IsRequired();

        builder.HasOne(i => i.Agente)
            .WithMany()
            .HasForeignKey("AgenteId")
            .IsRequired();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCFSeguroVeicular.API.Domain.Propostas.Entidades;

namespace PCFSeguroVeicular.API.Domain.Infra.Mappings;

public class PropostaMap : IEntityTypeConfiguration<Proposta>
{
    public void Configure(EntityTypeBuilder<Proposta> builder)
    {
        builder.ToTable("Propostas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.VlrTotalSeguro).HasColumnType("numeric(15,2)").IsRequired();
    }
}

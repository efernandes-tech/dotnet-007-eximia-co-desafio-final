using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlataformaClienteFinal.API.Dominio.CreditoConsignado.Entidades;

namespace PlataformaClienteFinal.API.Dominio.CreditoConsignado.Infraestrutura.Mapeamento;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("PropostaCliente");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(a => a.Rendimento)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Endereco)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Telefone)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(a => a.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Bloqueado)
            .HasDefaultValue(false)
            .IsRequired();
    }
}

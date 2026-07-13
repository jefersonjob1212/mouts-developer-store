using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SubsidiaryConfiguration : IEntityTypeConfiguration<Subsidiary>
{
    public void Configure(EntityTypeBuilder<Subsidiary> builder)
    {
        builder.ToTable("Subsidiaries");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.LegalName)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Cnpj)
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(x => x.TradeName)
            .HasMaxLength(150)
            .IsRequired();
        builder.HasIndex(x => x.TradeName);

        builder.Property(x => x.Address)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.State)
            .HasMaxLength(2)
            .IsRequired();
        
        builder.HasData(
            new Subsidiary
            {
                Id = Guid.NewGuid(),
                LegalName = "Tech System LTDA",
                TradeName = "Tech System São Paulo",
                Cnpj = "12345678000101",
                Address = "Av. Paulista, 1000 - Bela Vista",
                City = "São Paulo",
                State = "SP",
            },
            new Subsidiary
            {
                Id = Guid.NewGuid(),
                LegalName = "Tech System LTDA",
                TradeName = "Tech System Curitiba",
                Cnpj = "12345678000102",
                Address = "Rua XV de Novembro, 500 - Centro",
                City = "Curitiba",
                State = "PR",
            },
            new Subsidiary
            {
                Id = Guid.NewGuid(),
                LegalName = "Tech System LTDA",
                TradeName = "Tech System Florianópolis",
                Cnpj = "12345678000103",
                Address = "Av. Beira-Mar Norte, 2000 - Centro",
                City = "Florianópolis",
                State = "SC",
            }
        );
    }
}
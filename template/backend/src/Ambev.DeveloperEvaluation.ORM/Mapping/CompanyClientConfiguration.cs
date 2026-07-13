using Ambev.DeveloperEvaluation.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CompanyClientConfiguration : IEntityTypeConfiguration<CompanyClient>
{
    public void Configure(EntityTypeBuilder<CompanyClient> builder)
    {
        builder.Property(x => x.Cnpj)
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(x => x.LegalName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.TradeName)
            .HasMaxLength(200);
        
        builder.HasData(
            new CompanyClient
            {
                Id = Guid.NewGuid(),
                Cnpj = "70471641000100",
                LegalName = "Tech Solutions LTDA",
                TradeName = "Tech Solutions",
                Email = "contato@techsolutions.com",
                PhoneNumber = "4732101000",
                Address = "Avenida João Sacavem, 350, Centro",
                City = "Navegantes",
                State = "SC",
            },

            new CompanyClient
            {
                Id = Guid.NewGuid(),
                Cnpj = "72762733000174",
                LegalName = "Alpha Technology LTDA",
                TradeName = "Alpha Technology",
                Email = "contato@alphatechnology.com",
                PhoneNumber = "4731021040",
                Address = "Avenida XV de Novembro, 1450, Sala 301, Centro",
                City = "Blumenau",
                State = "SC",
            });
    }
}
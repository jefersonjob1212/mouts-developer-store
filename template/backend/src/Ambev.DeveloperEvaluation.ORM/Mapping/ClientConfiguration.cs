using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("Clients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(x => x.Email)
            .HasMaxLength(150);

        builder.Property(x => x.Address)
            .HasMaxLength(500);

        builder.Property(x => x.City)
            .HasMaxLength(50);

        builder.Property(x => x.State)
            .HasMaxLength(2);

        builder
            .HasDiscriminator<string>("ClientType")
            .HasValue<IndividualClient>("Individual")
            .HasValue<CompanyClient>("Company");
    }
}
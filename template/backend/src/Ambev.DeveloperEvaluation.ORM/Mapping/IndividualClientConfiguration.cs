using Ambev.DeveloperEvaluation.Domain;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class IndividualClientConfiguration : IEntityTypeConfiguration<IndividualClient>
{
    public void Configure(EntityTypeBuilder<IndividualClient> builder)
    {
        builder.Property(x => x.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(x => x.Gender)
            .HasConversion<string>();
        
        builder.HasData(new IndividualClient
            {
                Id = Guid.NewGuid(),
                Cpf = "19822639031",
                Name = "João da Silva",
                BornDate = new DateTime(1990, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                Gender = GenderIndividualClient.Male,
                Email = "joao@email.com",
                PhoneNumber = "47999990001",
                Address = "Rua Francisco Avelino Antunes, 45, Gravatá",
                City = "Navegantes",
                State = "SC",
            },
            
            new IndividualClient
            {
                Id = Guid.NewGuid(),
                Cpf = "91538032058",
                Name = "Antonieta Maria",
                BornDate = new DateTime(1990, 5, 10, 0, 0, 0, DateTimeKind.Utc),
                Gender = GenderIndividualClient.Female,
                Email = "antonieta.maria@email.com",
                PhoneNumber = "47999998888",
                Address = "Rua Hermann Tribess, 2500, Tribess",
                City = "Blumenau",
                State = "SC",
            });
    }
}
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();
        builder.HasIndex(x => x.Name);
        
        builder.Property(x => x.Code)
            .HasMaxLength(15)
            .IsRequired();
        builder.HasIndex(x => x.Code);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasIndex(x => x.Name);
        
        builder.HasData(
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Notebook Dell Inspiron 15",
                Description = "Notebook Dell Inspiron 15 i7 12th Gen RAM 32GB SSD 512GB",
                Code = "1000",
                Price = 4599.90m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Mouse Logitech MX Master 3S",
                Description = "Mouse Logitech MX Master 3S",
                Code = "1100",
                Price = 599.90m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Teclado Mecânico Keychron K2",
                Description = "Teclado Mecânico Keychron K2 RGB preto",
                Code = "1200",
                Price = 749.90m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Monitor LG UltraWide 29\"",
                Description = "Monitor LG UltraWide 29\" resolução 240hz",
                Code = "2000",
                Price = 1399.90m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Headset HyperX Cloud III",
                Description = "Headset Gamer HyperX Cloud III, Driver 53mm, USB, Multi Plataformas, Preto - 727A8AA",
                Code = "2100",
                Price = 849.90m
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Webcam Logitech C920 HD Pro",
                Description = "Webcam Full HD Logitech C920s com Microfone Embutido, Proteção de Privacidade, Widescreen 1080p, Compatível Logitech Capture - 960-001257",
                Code = "2200",
                Price = 489.90m
            }
        );
    }
}
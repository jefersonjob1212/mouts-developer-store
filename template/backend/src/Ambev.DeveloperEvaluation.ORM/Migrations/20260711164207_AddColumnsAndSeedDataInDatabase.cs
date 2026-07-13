using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnsAndSeedDataInDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullAddress",
                table: "Clients",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Subsidiaries",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cnpj",
                table: "Subsidiaries",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Clients",
                type: "character varying(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "City", "ClientType", "Cnpj", "Email", "LegalName", "PhoneNumber", "State", "TradeName" },
                values: new object[,]
                {
                    { new Guid("1424f419-4208-4e2d-abd0-07a4c2f7c218"), "Avenida João Sacavem, 350, Centro", "Navegantes", "Company", "70471641000100", "contato@techsolutions.com", "Tech Solutions LTDA", "4732101000", "SC", "Tech Solutions" },
                    { new Guid("198bd4f6-945b-4fa6-b38e-c6237e5c6ad0"), "Avenida XV de Novembro, 1450, Sala 301, Centro", "Blumenau", "Company", "72762733000174", "contato@alphatechnology.com", "Alpha Technology LTDA", "4731021040", "SC", "Alpha Technology" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "BornDate", "City", "ClientType", "Cpf", "Email", "Gender", "Name", "PhoneNumber", "State" },
                values: new object[,]
                {
                    { new Guid("4b888991-162f-4ed1-800d-474e13cb1a53"), "Rua Hermann Tribess, 2500, Tribess", new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Blumenau", "Individual", "91538032058", "antonieta.maria@email.com", "Female", "Antonieta Maria", "47999998888", "SC" },
                    { new Guid("f0a2a5c3-35bc-4edc-b9a4-5c27fd52a2c6"), "Rua Francisco Avelino Antunes, 45, Gravatá", new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Navegantes", "Individual", "19822639031", "joao@email.com", "Male", "João da Silva", "47999990001", "SC" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("11154f3b-4af8-4405-952f-ee965bb62057"), "1200", "Teclado Mecânico Keychron K2 RGB preto", "Teclado Mecânico Keychron K2", 749.90m },
                    { new Guid("1e4a418c-7b11-4838-9be0-1357a350cd41"), "2100", "Headset Gamer HyperX Cloud III, Driver 53mm, USB, Multi Plataformas, Preto - 727A8AA", "Headset HyperX Cloud III", 849.90m },
                    { new Guid("37a45032-d762-46f4-b6a9-181b65f3278a"), "2200", "Webcam Full HD Logitech C920s com Microfone Embutido, Proteção de Privacidade, Widescreen 1080p, Compatível Logitech Capture - 960-001257", "Webcam Logitech C920 HD Pro", 489.90m },
                    { new Guid("a8a9d057-8234-4e4f-8aec-a85819a4b075"), "1100", "Mouse Logitech MX Master 3S", "Mouse Logitech MX Master 3S", 599.90m },
                    { new Guid("be5e9de7-3fd3-4f1e-84bc-bca42a3521aa"), "2000", "Monitor LG UltraWide 29\" resolução 240hz", "Monitor LG UltraWide 29\"", 1399.90m },
                    { new Guid("fbb827f3-7dac-49fa-b24d-fbef6b13e6a8"), "1000", "Notebook Dell Inspiron 15 i7 12th Gen RAM 32GB SSD 512GB", "Notebook Dell Inspiron 15", 4599.90m }
                });

            migrationBuilder.InsertData(
                table: "Subsidiaries",
                columns: new[] { "Id", "Address", "City", "Cnpj", "LegalName", "State", "TradeName" },
                values: new object[,]
                {
                    { new Guid("43b664c1-79e9-4405-9161-bba9760b3bf0"), "Av. Paulista, 1000 - Bela Vista", "São Paulo", "12345678000101", "Tech System LTDA", "SP", "Tech System São Paulo" },
                    { new Guid("82275be2-026c-486c-a8b0-f6337b41b72b"), "Av. Beira-Mar Norte, 2000 - Centro", "Florianópolis", "12345678000103", "Tech System LTDA", "SC", "Tech System Florianópolis" },
                    { new Guid("debcbbc3-d904-4a35-96b7-1cbcdfe41723"), "Rua XV de Novembro, 500 - Centro", "Curitiba", "12345678000102", "Tech System LTDA", "PR", "Tech System Curitiba" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("1424f419-4208-4e2d-abd0-07a4c2f7c218"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("198bd4f6-945b-4fa6-b38e-c6237e5c6ad0"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("4b888991-162f-4ed1-800d-474e13cb1a53"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("f0a2a5c3-35bc-4edc-b9a4-5c27fd52a2c6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("11154f3b-4af8-4405-952f-ee965bb62057"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("1e4a418c-7b11-4838-9be0-1357a350cd41"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("37a45032-d762-46f4-b6a9-181b65f3278a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a8a9d057-8234-4e4f-8aec-a85819a4b075"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("be5e9de7-3fd3-4f1e-84bc-bca42a3521aa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fbb827f3-7dac-49fa-b24d-fbef6b13e6a8"));

            migrationBuilder.DeleteData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: new Guid("43b664c1-79e9-4405-9161-bba9760b3bf0"));

            migrationBuilder.DeleteData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: new Guid("82275be2-026c-486c-a8b0-f6337b41b72b"));

            migrationBuilder.DeleteData(
                table: "Subsidiaries",
                keyColumn: "Id",
                keyValue: new Guid("debcbbc3-d904-4a35-96b7-1cbcdfe41723"));

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Subsidiaries");

            migrationBuilder.DropColumn(
                name: "Cnpj",
                table: "Subsidiaries");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Clients",
                newName: "FullAddress");
        }
    }
}

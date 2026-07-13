using Ambev.DeveloperEvaluation.Domain.Factories;
using Ambev.DeveloperEvaluation.Domain.Interfaces;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Factories;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
        builder.Services.AddScoped<ISubsidiaryRepository, SubsidiaryRepository>();
        builder.Services.AddScoped<ISaleItemFactory, SaleItemFactoy>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
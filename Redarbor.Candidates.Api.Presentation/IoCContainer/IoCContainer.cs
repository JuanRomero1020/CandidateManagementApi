using System.Diagnostics.CodeAnalysis;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Redarbor.Candidates.Api.Business.Commands.Handlers;
using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Business.Services.Impl;
using Redarbor.Candidates.Api.Business.Services.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Infrastructure.DbContext;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Impl;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;
using Redarbor.Candidates.Api.Presentation.Serilog;

namespace Redarbor.Candidates.Api.Presentation.IoCContainer;

[ExcludeFromCodeCoverage]
public static class IoCContainer
{
    public static ContainerBuilder BuildContext(this ContainerBuilder builder, IConfiguration configuration)
    {
        var executionEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        RegisterClients(builder, configuration);
        RegisterCache(builder, configuration);
        RegisterRepositories(builder, configuration);
        RegisterServices(builder, configuration);
        builder.Register((_) => new LogCreator(configuration)).SingleInstance();
        return builder;
    }

    private static void RegisterCache(ContainerBuilder builder, IConfiguration configuration)
    {
    }

    private static void RegisterClients(ContainerBuilder builder, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.Register(c => new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options))
            .AsSelf()
            .InstancePerLifetimeScope();
    }

    private static void RegisterServices(ContainerBuilder builder, IConfiguration configuration)
    {
        builder.RegisterType<CandidateService>()
            .As<ICandidateService>()
            .InstancePerLifetimeScope();
        builder.RegisterType<CreateCandidateCommandHandler>()
            .As<ICommandHandler<CreateCandidateCommand>>()
            .InstancePerLifetimeScope();
    }

    private static void RegisterRepositories(ContainerBuilder builder, IConfiguration configuration)
    {
        builder.RegisterType<CandidateRepository>()
            .As<ICandidateRepository>()
            .InstancePerLifetimeScope();
    }
}
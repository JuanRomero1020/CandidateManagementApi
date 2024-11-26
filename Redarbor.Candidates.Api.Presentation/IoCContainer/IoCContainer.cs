using System.Diagnostics.CodeAnalysis;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Redarbor.Candidates.Api.Business.Commands.Handlers;
using Redarbor.Candidates.Api.Business.Commands.Interfaces;
using Redarbor.Candidates.Api.Business.Services.Impl;
using Redarbor.Candidates.Api.Business.Services.Interfaces;
using Redarbor.Candidates.Api.Domain.Commands.Create;
using Redarbor.Candidates.Api.Domain.Commands.Update;
using Redarbor.Candidates.Api.Domain.Commands.Delete;
using Redarbor.Candidates.Api.Infrastructure.DbContext;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Impl;
using Redarbor.Candidates.Api.Infrastructure.Repositories.Interfaces;
using Redarbor.Candidates.Api.Presentation.Serilog;
using Serilog;

namespace Redarbor.Candidates.Api.Presentation.IoCContainer;

[ExcludeFromCodeCoverage]
public static class IoCContainer
{
    public static ContainerBuilder BuildContext(this ContainerBuilder builder, IConfiguration configuration)
    {
        Log.Debug("Building Autofac dependencies");
        RegisterClients(builder, configuration);
        RegisterRepositories(builder, configuration);
        RegisterServices(builder, configuration);
        RegisterHandlers(builder, configuration);
        builder.Register((_) => new LogCreator(configuration)).SingleInstance();
        return builder;
    }


    private static void RegisterClients(ContainerBuilder builder, IConfiguration configuration)
    {
        Log.Debug("Building Autofac clients dependencies");
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        builder.Register(c => new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options))
            .AsSelf()
            .InstancePerLifetimeScope();
    }

    private static void RegisterServices(ContainerBuilder builder, IConfiguration configuration)
    {
        Log.Debug("Building Autofac Services dependencies");
        builder.RegisterType<CandidateService>()
            .As<ICandidateService>()
            .InstancePerLifetimeScope();
    }

    private static void RegisterHandlers(ContainerBuilder builder, IConfiguration configuration)
    {
        Log.Debug("Building Autofac handlers from Services dependencies");
        builder.RegisterType<CreateCandidateCommandHandler>()
            .As<ICommandHandler<CreateCandidateCommand>>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UpdateCandidateCommandHandler>()
            .As<ICommandHandler<UpdateCandidateCommand>>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DeleteCandidateCommandHandler>()
            .As<ICommandHandler<DeleteCandidateCommand>>()
            .InstancePerLifetimeScope();
    }

    private static void RegisterRepositories(ContainerBuilder builder, IConfiguration configuration)
    {
        Log.Debug("Building Autofac Repository dependencies");
        builder.RegisterType<CandidateRepository>()
            .As<ICandidateRepository>()
            .InstancePerLifetimeScope();
    }
}
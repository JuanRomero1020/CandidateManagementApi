using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Redarbor.Candidates.Api.Presentation.IoCContainer;
using Redarbor.Candidates.Api.Presentation.Mappers;
using Redarbor.Candidates.Api.Presentation.Serilog;
using Redarbor.Candidates.Api.Presentation.Validators;
using Serilog;

namespace Redarbor.Candidates.Api.Presentation;

[ExcludeFromCodeCoverage]
public static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureWebHost(builder);
        ConfigureServices(builder.Services, builder.Environment);
        ConfigureRedisCache(builder.Services, builder.Configuration);
        var app = ConfigureWebApp(builder);
        await app.RunAsync();
    }

    private static void ConfigureRedisCache(IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetSection("Redis")["ConnectionString"];
        });
    }

    private static void ConfigureWebHost(WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Host
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureAppConfiguration(UseParameterStore)
            .ConfigureContainer<ContainerBuilder>((context, container) =>
                container.BuildContext(context.Configuration)
            )
            .UseSerilog((_, provider, loggerConfiguration) => BuildLogger(provider, loggerConfiguration));
    }

    private static WebApplication ConfigureWebApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseRouting();
        app.UseCors();
        if (builder.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "backend v1"));
        }

        app.UseEndpoints(endpoints =>
        {
            ArgumentNullException.ThrowIfNull(endpoints);
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/health");
        });
        return app;
    }

    private static void ConfigureServices(IServiceCollection services, IWebHostEnvironment webHostEnvironment)
    {
        ConfigureValidators(services);
        services.AddAutoMapper(typeof(MappingProfileCandidateMappe));
        services.AddHttpContextAccessor();
        services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .AddNewtonsoftJson(options => { options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; });
        services.AddHealthChecks();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.SetIsOriginAllowed(origin =>
                    {
                        var host = new Uri(origin).Host;
                        return host.Contains("localhost") || host.Contains("redarborapp.com");
                    });
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
        });
        services.AddLogging();
        if (webHostEnvironment.IsDevelopment())
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API endpoints (ONLY FOR DEVELOPMENT)"
                });
            });
        }
    }

    private static void ConfigureValidators(IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CandidateValidator>();
    }

    private static void BuildLogger(IServiceProvider provider, LoggerConfiguration loggerConfiguration)
    {
        provider.GetRequiredService<LogCreator>();
        ChangeToken.OnChange(() =>
            {
                var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(60));
                var cancellationChangeToken = new CancellationChangeToken(cancellationTokenSource.Token);
                return cancellationChangeToken;
            },
            LogCreator.UpdateLogLevel);

        LogCreator.ConfigureLogging(loggerConfiguration);
    }

    private static void UseParameterStore(IConfigurationBuilder configurationBuilder)
    {
        // configurationBuilder
        //     .AddSystemsManager(
        //         Environment.GetEnvironmentVariable("PARAMETER_STORE_PATH"),
        //         TimeSpan.FromSeconds(60));
    }
}
using System.Diagnostics.CodeAnalysis;
using Serilog;

namespace Redarbor.Candidates.Api.Presentation.Serilog;

[ExcludeFromCodeCoverage]
public class LogCreator
{
    private static LoggingLevelSwitchFromConfig? _levelSwitchFromConfig;
    private static LoggingLevelSwitchFromConfig? _aspLoggingLevel;

    public LogCreator(IConfiguration configuration)
    {
        _levelSwitchFromConfig = new LoggingLevelSwitchFromConfig("LoggingLevel", configuration);
        _aspLoggingLevel = new LoggingLevelSwitchFromConfig("AspLoggingLevel", configuration);
    }

    public static void UpdateLogLevel()
    {
        _levelSwitchFromConfig?.UpdateLoggingLevel();
        _aspLoggingLevel?.UpdateLoggingLevel();
    }

    public static void ConfigureLogging(LoggerConfiguration loggerConfiguration)
    {
        loggerConfiguration
            .MinimumLevel.ControlledBy(_levelSwitchFromConfig)
            .MinimumLevel.Override("Microsoft.AspNetCore", _aspLoggingLevel)
            .Enrich.WithCorrelationId()
            .WriteTo.Async(
                (write) => write.Console(
                    outputTemplate:
                    "{Timestamp:HH:mm:ss.fff} ({CorrelationId}) [{Level}]  {Message}, {Exception} {NewLine}"));
    }
}
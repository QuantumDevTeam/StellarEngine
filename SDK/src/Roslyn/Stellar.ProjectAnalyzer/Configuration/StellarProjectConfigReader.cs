using System;
using Newtonsoft.Json;

namespace Stellar.ProjectAnalyzer.Configuration;

internal readonly struct ConfigReadResult
{
    public bool IsSuccess { get; }
    public StellarAnalyzerConfigFile? Config { get; }
    public string? ErrorMessage { get; }

    private ConfigReadResult(StellarAnalyzerConfigFile config)
    {
        IsSuccess = true;
        Config = config;
        ErrorMessage = null;
    }

    private ConfigReadResult(string errorMessage)
    {
        IsSuccess = false;
        Config = null;
        ErrorMessage = errorMessage;
    }

    public static ConfigReadResult Success(StellarAnalyzerConfigFile config) => new(config);
    public static ConfigReadResult Failure(string errorMessage) => new(errorMessage);
}

internal static class StellarProjectConfigReader
{
    public static ConfigReadResult TryRead(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return ConfigReadResult.Failure("Config file is empty.");

        try
        {
            var file = JsonConvert.DeserializeObject<StellarAnalyzerConfigFile>(json);

            return file is not null
                ? ConfigReadResult.Success(file)
                : ConfigReadResult.Failure("Deserialization returned null.");
        }
        catch (JsonException ex)
        {
            return ConfigReadResult.Failure($"JSON parse error: {ex.Message}");
        }
        catch (Exception ex)
        {
            return ConfigReadResult.Failure($"Unexpected error: {ex.Message}");
        }
    }
}
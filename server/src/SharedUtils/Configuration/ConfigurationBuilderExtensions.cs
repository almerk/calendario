using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
namespace SharedUtils.Configuration
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddEnvFileConfiguration([NotNull] this IConfigurationBuilder builder, string envFilePath) 
        => builder.Add(new EnvFileConfigurationSource(envFilePath));
    }
}
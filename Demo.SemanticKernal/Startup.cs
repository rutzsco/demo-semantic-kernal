
using Demo.SemanticKernal.Services.Skills;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using System;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Demo.SemanticKernal.Startup))]

namespace Demo.SemanticKernal
{
    public class Startup : FunctionsStartup
    {
        private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddEnvironmentVariables()
            .Build();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IKernel>(sp =>
            {
                IKernel kernel = Kernel.Builder
                .WithAzureChatCompletionService(configuration["AZURE_OPENAI_DEPLOYMENT"], configuration["AZURE_OPENAI_ENDPOINT"], configuration["AZURE_OPENAI_KEY"])
                .Build();

                return kernel;
            });
        }
    }
}
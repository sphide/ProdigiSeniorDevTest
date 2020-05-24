using ColourMatcher.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ColourMatcher.Startup))]
namespace ColourMatcher
{
    /// <summary>
    /// Enabling DI by registering services
    /// see https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
    /// </summary>
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IImageDownloader, Implementations.ImageDownloader>();
            builder.Services.AddSingleton<IColourReferencesProvider, Implementations.ColourReferencesProvider>();
            builder.Services.AddTransient<IDominantColourFinder, Implementations.DominantColourFinder>();
            builder.Services.AddTransient<IColourReferenceMatcher, Implementations.ColourReferenceMatcher>();
        }
    }
}

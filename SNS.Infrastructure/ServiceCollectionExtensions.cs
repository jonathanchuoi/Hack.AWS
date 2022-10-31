using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SNS.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmaznoSNS(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(options);
        services.AddAWSService<IAmazonSimpleNotificationService>();

        return services;
    }
}
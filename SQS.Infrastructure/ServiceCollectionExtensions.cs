using Amazon.Extensions.NETCore.Setup;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SQS.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonSQS(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(new AWSOptions());
        services.AddAWSService<IAmazonSQS>();

        return services;
    }
}
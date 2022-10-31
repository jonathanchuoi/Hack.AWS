using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SNS.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmaznoSNS(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BasicAwsCredentialOptions>(configuration.GetSection(BasicAwsCredentialOptions.SectionName));
        var options = configuration.GetSection(BasicAwsCredentialOptions.SectionName)
            .Get<BasicAwsCredentialOptions>();

        var config = new AmazonSimpleNotificationServiceConfig
        {
            ServiceURL = options.ServiceUrl
        };

        var cred = new BasicAWSCredentials(options.AccessKeyId, options.SecretAccessKey);
        var client = new AmazonSimpleNotificationServiceClient(cred, config);

        services.AddSingleton<IAmazonSimpleNotificationService>(client);
        return services;
    }
}
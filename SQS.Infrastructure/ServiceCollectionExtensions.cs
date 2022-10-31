using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SQS.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonSQS(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BasicAwsCredentialOptions>(configuration.GetSection(BasicAwsCredentialOptions.SectionName));
        var options = configuration.GetSection(BasicAwsCredentialOptions.SectionName)
            .Get<BasicAwsCredentialOptions>();
        var config = new AmazonSQSConfig
        {
            ServiceURL = options.ServiceUrl
        };
        var cred = new BasicAWSCredentials(options.AccessKeyId, options.SecretAccessKey);
        var client = new AmazonSQSClient(cred, config);

        services.AddSingleton<IAmazonSQS>(client);

        return services;
    }
}
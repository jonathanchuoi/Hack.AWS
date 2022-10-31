using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SQS.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazonSQS(this IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine("A");
        services.Configure<BasicAwsCredentialOptions>(configuration.GetSection(BasicAwsCredentialOptions.SectionName));
        var options = configuration.GetSection(BasicAwsCredentialOptions.SectionName)
            .Get<BasicAwsCredentialOptions>();
      Console.WriteLine("D");
        var config = new AmazonSQSConfig
        {
            ServiceURL = options.ServiceUrl
        };
              Console.WriteLine("E");
        var cred = new BasicAWSCredentials(options.AccessKeyId, options.SecretAccessKey);
              Console.WriteLine("F");
        var client = new AmazonSQSClient(cred, config);
      Console.WriteLine("B");
        services.AddSingleton<IAmazonSQS>(client);
      Console.WriteLine("C");
        return services;
    }
}
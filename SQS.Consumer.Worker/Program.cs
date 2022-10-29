using SQS.Consumer.Worker;
using SQS.Infrastructure;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((builder, services) => { services
        .AddAmazonSQS(builder.Configuration)
        .AddHostedService<Worker>(); })
    .Build();

await host.RunAsync();
using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQS.Consumer.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IAmazonSQS _sqs;

    public Worker(ILogger<Worker> logger, IAmazonSQS sqs)
    {
        _logger = logger;
        _sqs = sqs;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var response = await _sqs.CreateQueueAsync(request: new CreateQueueRequest
            {
                QueueName = "theQueue"
            });
            var a = response.QueueUrl;

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
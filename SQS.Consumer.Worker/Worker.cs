using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using SQS.Infrastructure;

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

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var messages = await _sqs.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                MaxNumberOfMessages = 1,
                QueueUrl = "http://localhost:4566/000000000000/APIQueue",
                WaitTimeSeconds = 5 //long poll
            }, cancellationToken);

            foreach (var message in messages.Messages)
            {
                var body = JsonSerializer.Deserialize<SQSMessage>(message.Body);
                Console.WriteLine(body!.Message);
            }
            
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, cancellationToken);
        }
    }
}
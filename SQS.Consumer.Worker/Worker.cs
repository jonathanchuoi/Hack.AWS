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

    const string QueueUrl = "http://localhost:4566/000000000000/APIQueue";

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var messages = await _sqs.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                MaxNumberOfMessages = 1,
                QueueUrl = QueueUrl,
                WaitTimeSeconds = 5 //long poll
            }, cancellationToken);

            foreach (var message in messages.Messages)
            {
                var body = JsonSerializer.Deserialize<SQSMessage>(message.Body);
                Console.WriteLine(body!.Message);
                var delete = await _sqs.DeleteMessageAsync(new DeleteMessageRequest(QueueUrl, message.ReceiptHandle),
                    cancellationToken);
                Console.WriteLine(delete.HttpStatusCode);
            }
        }
    }
}
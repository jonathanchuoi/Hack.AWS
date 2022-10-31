using System.Text.Json;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.AspNetCore.Mvc;
using SQS.Infrastructure;

namespace SNS.Producer.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private const string TopicName = "Hi";
    private const string QueueUrl = "http://localhost:4566/000000000000/APIQueue";
    private readonly IAmazonSimpleNotificationService _sns;
    private readonly IAmazonSQS _sqs;

    public MessageController(IAmazonSimpleNotificationService sns, IAmazonSQS sqs)
    {
        _sns = sns;
        _sqs = sqs;
    }

    // GET: api/Message
    [HttpGet]
    public async Task<IEnumerable<string?>> Get()
    {
        var message = await _sqs.ReceiveMessageAsync(new ReceiveMessageRequest
        {
            MaxNumberOfMessages = 1,
            QueueUrl = QueueUrl
        });

        return message.Messages
            .Where(q => q is not null)
            .Select(q => JsonSerializer.Deserialize<SQSMessage>(q.Body))
            .Select(q => q!.Message);
    }

    // GET: api/Message/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
        return "value";
    }

    // POST: api/Message
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string value)
    {
        var topic = await _sns.FindTopicAsync(TopicName);
        if (topic is null) return StatusCode(StatusCodes.Status500InternalServerError);

        var publish = await _sns.PublishAsync(topic.TopicArn, value);
        return StatusCode((int)publish.HttpStatusCode);
    }

    // PUT: api/Message/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE: api/Message/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
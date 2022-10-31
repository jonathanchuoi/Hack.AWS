using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.SQS;
using SNS.Infrastructure;
using SQS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddAmazonSQS(builder.Configuration)
    .AddAmaznoSNS(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    const string topicName = "Hi";
    var sns = scope.ServiceProvider.GetRequiredService<IAmazonSimpleNotificationService>();
    var request = new CreateTopicRequest(topicName);

    var topic = await sns.CreateTopicAsync(request);

    var sqs = scope.ServiceProvider.GetRequiredService<IAmazonSQS>();
    var queue = await sqs.CreateQueueAsync("APIQueue");

    await sns.SubscribeQueueAsync(topic.TopicArn, sqs, queue.QueueUrl);
}


app.Run();
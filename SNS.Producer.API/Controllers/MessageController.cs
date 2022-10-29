using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SNS.Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IAmazonSimpleNotificationService _sns;
        private const string TopicName = "Hi";

        public MessageController(IAmazonSimpleNotificationService sns)
        {
            _sns = sns;
        }
        
        // GET: api/Message
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var request = new CreateTopicRequest
            {
                Name = TopicName,
            };

            var response = await _sns.CreateTopicAsync(request);

            return Enumerable.Empty<string>();
        }

        // GET: api/Message/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Message
        [HttpPost]
        public async void Post([FromBody] string value)
        {
            
            var request = new CreateTopicRequest
            {
                Name = TopicName,
            };

            var response = await _sns.CreateTopicAsync(request);
            await _sns.PublishAsync(response.TopicArn, value);
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
}

using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Azure.Messaging.EventGrid;

namespace MoodPulseFuncApp
{
    public static class HandleEventGrid
    {
        [FunctionName("HandleEventGrid")]
        public static async Task Run(
            [EventGridTrigger] EventGridEvent eventGridEvent,
            ILogger log)
        {
            log.LogInformation($"Event received: {eventGridEvent.EventType}");
            // Convert BinaryData to JObject
            var json = eventGridEvent.Data.ToString(); // or .ToStream() if needed
            var jObject = JObject.Parse(json);
            // Parse event data
            //JObject data = eventGridEvent.Data as JObject;
            log.LogInformation($"Event data: {jObject?.ToString()}");

            // Add your logic here
            await Task.CompletedTask;
        }
    }
}

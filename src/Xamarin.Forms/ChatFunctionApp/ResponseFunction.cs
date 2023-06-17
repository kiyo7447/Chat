using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;

namespace ChatFunctionApp
{
    public class ResponseFunction
    {
        [FunctionName("ResponseFunction")]
        public void Run(
            [ServiceBusTrigger("chatqueue", Connection = "ServiceBusListenConnection")] string myQueueItem,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var connectionString = System.Environment.GetEnvironmentVariable("TableStorageConnectionSetting");
            var tableClient = new TableClient(connectionString, "chat");

            var entity = new TableEntity("partition1", Guid.NewGuid().ToString())
            {
                { "message", myQueueItem}
            };
            try
            {
                tableClient.AddEntityAsync(entity);
                log.LogInformation($"メッセージをTable Storageに保存しました。");
            }
            catch (Exception ex)
            {
                log.LogError($"Error Message:{ex.ToString()}");
                throw;
            }


        }
    }
}

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

            var tableClient = new TableClient("DefaultEndpointsProtocol=https;AccountName=openaichatstorage2;AccountKey=cTl1sp+iRdbtKAKhFDXGjwCoJjaTXfzd+I/kkmCpVQeTZniPqNix1flgiESEg4v0Zrj2u4bsnv6o+AStGM2BjQ==;EndpointSuffix=core.windows.net", "chat");

            var entity = new TableEntity("partition1", Guid.NewGuid().ToString())
            {
                { "message", myQueueItem}
            };
            try
            {
                tableClient.AddEntityAsync(entity);

            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                throw;
            }


        }
    }
}

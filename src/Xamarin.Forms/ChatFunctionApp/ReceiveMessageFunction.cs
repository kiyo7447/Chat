using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DataModel;
using Azure.Messaging.ServiceBus;

namespace ChatFunctionApp
{
    public static class ReceiveMessageFunction
    {



        [FunctionName("ReceiveMessageFunction")]
        public static async Task<IActionResult> Run(
            //HttpTrigger��MyTalk�N���X���󂯎��
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            //Azure Service Bus�ւ̐ڑ���������w��
            [ServiceBus("chatqueue", Connection = "ServiceBusSendConnection")] IAsyncCollector<MyChat> outputQueueItem,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation("���b�Z�[�W��Azure Service Bus�ɑ��M���܂����B");

            var data = JsonConvert.DeserializeObject<MyChat>(requestBody);

            // ���b�Z�[�W���쐬���đ��M
            await outputQueueItem.AddAsync(data);

            string responseMessage = $"Received Message: {data.Property1}, {data.Property2}";
            log.LogInformation($"Received Message:{responseMessage}");

            return new OkObjectResult(responseMessage);
        }
    }
}

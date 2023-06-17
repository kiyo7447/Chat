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
            //HttpTriggerでMyTalkクラスを受け取る
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            //Azure Service Busへの接続文字列を指定
            [ServiceBus("chatqueue", Connection = "ServiceBusSendConnection")] IAsyncCollector<MyChat> outputQueueItem,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation("メッセージをAzure Service Busに送信しました。");

            var data = JsonConvert.DeserializeObject<MyChat>(requestBody);

            // メッセージを作成して送信
            await outputQueueItem.AddAsync(data);

            string responseMessage = $"Received Message: {data.Property1}, {data.Property2}";
            log.LogInformation($"Received Message:{responseMessage}");

            return new OkObjectResult(responseMessage);
        }
    }
}

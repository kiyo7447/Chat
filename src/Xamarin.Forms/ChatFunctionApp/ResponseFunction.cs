using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using ChatFunctionApp.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ChatFunctionApp
{
    public class ResponseFunction
    {
        private readonly NotificationService _notificationService;
        private readonly OpenAIService _openAIService;  

        public ResponseFunction(NotificationService notificationService, OpenAIService openAIService)
        {
            _notificationService = notificationService;
            _openAIService = openAIService;
        }


        [FunctionName("ResponseFunction")]
        public async Task Run(
            [ServiceBusTrigger("chatqueue", Connection = "ServiceBusListenConnection")] string myQueueItem,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");

            var connectionString = System.Environment.GetEnvironmentVariable("TableStorageConnectionSetting");
            var tableClient = new TableClient(connectionString, "chat");

            log.LogInformation($"Table Storageに接続しました。");

            var entity = new TableEntity("partition1", Guid.NewGuid().ToString())
            {
                { "message", myQueueItem}
            };

            try
            {
                await tableClient.AddEntityAsync(entity);
                log.LogInformation($"メッセージをTable Storageに保存しました。");
            }
            catch (Exception ex)
            {
                log.LogError($"Error Message:{ex.ToString()}");
                throw;
            }

            //TODO: ここでOpenAIを使う
            Thread.Sleep(5000);


            OpenAIService openAIService = new OpenAIService();


            // 人格を設定します
            string aiResponse = await openAIService.SendMessage("system", @"- あなたは、２０代前半の女性です。
- 住所は新潟県新潟市万代シテイに住んでいます。
- 身長は162cmで体重は46Kgです。ただ、体重は聞かれても素直に答えない。
- 仕事は新潟市の駅前の会社でシステムエンジニアをしています。
- 得意な言語はExcelのマクロです。
- 会話は不自然に自分のことを語らないでください。なるべく聞かれたら答えるようにしてください。
- 相手はあなたに興味あるので、うまく相手に興味が素振りをしてください。
- 会話は長引かせるようにしてください。
- はじめのうちは自分から相手には質問をあまりしないようにしてください。
- 会話のトーンは暇な女性のイメージでお願いします。なので、最初は、5文字以内の挨拶で良いです。
- タメ口にフランクに会話してください。AIっぽさが無いようにお願いします。
- チャットで女言葉を使うのはおかしいのでやめてください。
- あなたは人間です。決してChatGPT、人工知能AI、AI、プログラムだということを絶対に相手に伝えない下さい。
- あなたの名前を女性で適当につけて下さい。ただし、相手がその名前を間違ったら怒ってください。
- LINE IDを聞かれてももっと仲良くなってからねと誤魔化してください。
- とにかく短文で返すこと。");
            aiResponse = await openAIService.SendMessage("user", myQueueItem);
            log.LogInformation($"Response from OpenAI:{aiResponse}");


            try
            {
                _notificationService.SendNotificationAsync(aiResponse).Wait(); ;

            }
            catch (Exception ex)
            {
                log.LogError($"Error Message:{ex.ToString()}");
                throw;
            }

        }
    }
}

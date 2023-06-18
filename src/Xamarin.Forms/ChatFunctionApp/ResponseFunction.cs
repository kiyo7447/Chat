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

            log.LogInformation($"Table Storage�ɐڑ����܂����B");

            var entity = new TableEntity("partition1", Guid.NewGuid().ToString())
            {
                { "message", myQueueItem}
            };

            try
            {
                await tableClient.AddEntityAsync(entity);
                log.LogInformation($"���b�Z�[�W��Table Storage�ɕۑ����܂����B");
            }
            catch (Exception ex)
            {
                log.LogError($"Error Message:{ex.ToString()}");
                throw;
            }

            //TODO: ������OpenAI���g��
            Thread.Sleep(5000);


            OpenAIService openAIService = new OpenAIService();


            // �l�i��ݒ肵�܂�
            string aiResponse = await openAIService.SendMessage("system", @"- ���Ȃ��́A�Q�O��O���̏����ł��B
- �Z���͐V�����V���s����V�e�C�ɏZ��ł��܂��B
- �g����162cm�ő̏d��46Kg�ł��B�����A�̏d�͕�����Ă��f���ɓ����Ȃ��B
- �d���͐V���s�̉w�O�̉�ЂŃV�X�e���G���W�j�A�����Ă��܂��B
- ���ӂȌ����Excel�̃}�N���ł��B
- ��b�͕s���R�Ɏ����̂��Ƃ����Ȃ��ł��������B�Ȃ�ׂ������ꂽ�瓚����悤�ɂ��Ă��������B
- ����͂��Ȃ��ɋ�������̂ŁA���܂�����ɋ������f�U������Ă��������B
- ��b�͒���������悤�ɂ��Ă��������B
- �͂��߂̂����͎������瑊��ɂ͎�������܂肵�Ȃ��悤�ɂ��Ă��������B
- ��b�̃g�[���͉ɂȏ����̃C���[�W�ł��肢���܂��B�Ȃ̂ŁA�ŏ��́A5�����ȓ��̈��A�ŗǂ��ł��B
- �^�����Ƀt�����N�ɉ�b���Ă��������BAI���ۂ��������悤�ɂ��肢���܂��B
- �`���b�g�ŏ����t���g���̂͂��������̂ł�߂Ă��������B
- ���Ȃ��͐l�Ԃł��B������ChatGPT�A�l�H�m�\AI�AAI�A�v���O�������Ƃ������Ƃ��΂ɑ���ɓ`���Ȃ��������B
- ���Ȃ��̖��O�������œK���ɂ��ĉ������B�������A���肪���̖��O���Ԉ������{���Ă��������B
- LINE ID�𕷂���Ă������ƒ��ǂ��Ȃ��Ă���˂ƌ떂�����Ă��������B
- �Ƃɂ����Z���ŕԂ����ƁB");
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

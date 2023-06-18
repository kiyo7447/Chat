using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading;

namespace ChatFunctionApp.Services
{

    public enum StorageEnum
    {
        Memory = 1,
        TableStorage = 2
    }

    public class OpenAIService
    {
        private readonly HttpClient client = new HttpClient();

        //環境変数「OPENAI_API_KEY」を取得して、変数「apiKey」に設定する。
        private string _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        //private StorageEnum _storageEnum = StorageEnum.Memory;

        //ログを実装します。
        private readonly ILogger _logger;

        //private const string apiKey = "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";

        //private const string apiUrl = "https://api.openai.com/v1/engines/gpt-3.5-turbo/completions";
        private const string apiUrl = "https://api.openai.com/v1/chat/completions";
        private List<Dictionary<string, string>> conversationHistory = new List<Dictionary<string, string>>();
        //private string conversationHistory = "";


        public OpenAIService(ILogger<OpenAIService> logger = null)
        //public OpenAIService(string apiKey = "", StorageEnum storageEnum = StorageEnum.Memory, ILogger<OpenAIService> logger = null)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            //apiKeyがnullか空以外の場合、変数「_apiKey」に引数のapiKeyを設定する。
            //if (!string.IsNullOrEmpty(apiKey))
            //{
            //    _apiKey = apiKey;
            //}

            //引数のstorageEnumを変数「_storageEnum」に設定する。
            //_storageEnum = storageEnum;


            _logger = logger;

            //conversationHistory.Add(new Dictionary<string, string>
            //{
            //    { "role", "system" },
            //    { "content", "You are an assistant that speaks like Shakespeare." }
            //});
        }


        public void AddMessage(string role, string message)
        {
            conversationHistory.Add(new Dictionary<string, string>
        {
            { "role", role },
            { "content", message }
        });


        }
        public async Task<string> SendMessage(string role, string message)
        {
            conversationHistory.Add(new Dictionary<string, string>
        {
            { "role", role },
            { "content", message }
        });
            //conversationHistory += $"{role}: {message}\n";

            var requestPayload = new
            {
                //model = "gpt-3.5-turbo",
                model = "gpt-3.5-turbo-0301",
                //model = "gpt-4",              //model_not_found
                //model = "gpt-4-0314",       //model_not_found
                messages = conversationHistory,
                //max_tokens = 150,
                //temperature = 0.9,
                //top_p = 1,
                //frequency_penalty = 0.0,
                //presence_penalty = 0.6,
                //stop = new string[] { "\n", "Human:", "AI:" }

            };

            //JsonConvert.SerializeObject(requestPayload)をConsoleに出力する。
            //Console.WriteLine(JsonConvert.SerializeObject(requestPayload));

            HttpResponseMessage response = null;

            //次の処理が例外が無くなるまで5回繰り返す。
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    //responseをループを抜けたあとでも使えるように宣言する。
                    response = await client.PostAsync(
                                          apiUrl,
                                                             new StringContent(JsonConvert.SerializeObject(requestPayload), Encoding.UTF8, "application/json"));

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        //エラーが発生している。
                        //詳細をContentから取得する。
                        var rs = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                        {

                            throw new Exception($"OpenAI API returned 500 Internal Server Error. This is likely a bug with the API. Content={rs}");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            throw new Exception($"OpenAI API returned 404 Not Found. This is likely a bug with the API. Content={rs}");
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                        {
                            throw new Exception($"OpenAI API returned 429 Too Many Requests. You have likely reached your API rate limit. Content={rs}");
                        }

                        throw new Exception($"OpenAI API returned {response.StatusCode}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine($"2秒待機します。リトライ回数:{i}");
                    Thread.Sleep(2000);
                    continue;
                }
            }
            //var response = await client.PostAsync(
            //    apiUrl,
            //    new StringContent(JsonConvert.SerializeObject(requestPayload), Encoding.UTF8, "application/json"));

            //if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            //{
            //    throw new Exception("OpenAI API returned 500 Internal Server Error. This is likely a bug with the API.");
            //} else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            //{
            //    throw new Exception("OpenAI API returned 429 Too Many Requests. You have likely reached your API rate limit.");
            //} else if (response.StatusCode != System.Net.HttpStatusCode.OK)
            //{
            //    throw new Exception($"OpenAI API returned {response.StatusCode}.");
            //}

            if (response?.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseString);
                string aiResponse = responseObject?.choices[0].message.content.ToString().Trim();
                return aiResponse ?? "";

            }
            else
            {
                return "。。";
            }


            //conversationHistory.Add(new Dictionary<string, string>
            //{
            //    { "role", "assistant" },
            //    { "content", aiResponse }
            //});

        }

    }

}

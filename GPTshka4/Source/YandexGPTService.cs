using apiTest;
using apiTest.Models;
using GPTshka4.Models.YandexGPTModels;
using static System.Net.Mime.MediaTypeNames;

namespace GPTshka4.Source
{
    public class YandexGPTService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly YandexGPTSettings _yandexGPTSettings;
        public YandexGPTService(IHttpClientFactory httpClientFactory,YandexGPTSettings yandexGPTSettings) 
        { 

            _httpClientFactory = httpClientFactory;
            _yandexGPTSettings= yandexGPTSettings;

        }

        public async Task<Answer> SendRequest(string requestText)
        {




            return new Answer();
        }

        private HttpRequestMessage BuildRequest(string requestText)
        {
            RequestBody requestBody = new RequestBody();
            requestBody.completionOptions = new CompletionOptions();
            requestBody.completionOptions.temperature = 0.6f;
            requestBody.completionOptions.stream = false;
            requestBody.completionOptions.maxTokens = 2000;
            requestBody.modelUri = "gpt://b1g013mvdvoq090dg2pi/yandexgpt-lite";

            requestBody.messages = new Message[] { new Message() { text = requestText, role = "user" } };

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add("Authorization", "Bearer t1.9euelZrJmsuKyJKNzYuVj5aczp3Ize3rnpWakZXLjMeWnMrOk5GUy5zHmJ3l8_cFORdL-e9zG0g2_d3z90VnFEv573MbSDb9zef1656VmozGy57PzpOVnY-ZzsrJk8nO7_zF656VmozGy57PzpOVnY-ZzsrJk8nO.pQxGzETc-po9NL3DpB8pNjjLjPq-b4OQmBYqV_wYrmMXM9W2HxztoLQB_mCNdmj7yLT8Gc8IRZHPxBr7j-_DCw");
            httpRequestMessage.Headers.Add("x-folder-id", "b1g013mvdvoq090dg2pi");
            httpRequestMessage.Method = HttpMethod.Post;
            HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(CreateRequestBody(text)));

            httpRequestMessage.Content = httpContent;

            httpRequestMessage.RequestUri = new Uri("https://llm.api.cloud.yandex.net/foundationModels/v1/completion");

        }

    }
}

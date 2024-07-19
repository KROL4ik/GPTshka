using apiTest;
using apiTest.Models;
using GPTshka4.Models.YandexGPTModels;
using Newtonsoft.Json;
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
            string result;
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage responseMessage = await client.SendAsync(BuildRequest(requestText));
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            Answer answer = JsonConvert.DeserializeObject<Answer>(result);
            return answer;

        }

        private HttpRequestMessage BuildRequest(string requestText)
        {
        

            RequestBody requestBody = new RequestBody();
            requestBody.completionOptions = new CompletionOptions();
            requestBody.completionOptions.temperature = 0.6f;
            requestBody.completionOptions.stream = false;
            requestBody.completionOptions.maxTokens = 2000;
            requestBody.modelUri = _yandexGPTSettings.model_uri;

            requestBody.messages = new Message[] { new Message() { text = requestText, role = "user" } };

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Headers.Add(_yandexGPTSettings.Authorization.Key,_yandexGPTSettings.Authorization.Value);
            httpRequestMessage.Headers.Add(_yandexGPTSettings.x_folder_id.Key,_yandexGPTSettings.x_folder_id.Value);
            httpRequestMessage.Method = HttpMethod.Post;
            HttpContent httpContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody));

            httpRequestMessage.Content = httpContent;

            httpRequestMessage.RequestUri = new Uri(_yandexGPTSettings.request_uri);

            return httpRequestMessage;


        }

    }
}

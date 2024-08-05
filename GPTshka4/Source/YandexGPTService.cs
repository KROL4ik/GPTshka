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
        public YandexGPTService(IHttpClientFactory httpClientFactory, YandexGPTSettings yandexGPTSettings)
        {

            _httpClientFactory = httpClientFactory;
            _yandexGPTSettings = yandexGPTSettings;
        }

        public async Task<Answer> SendRequest(string requestText, CompletionOptions completion)
        {
            string result;
            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                HttpResponseMessage responseMessage = await client.SendAsync(BuildRequest(requestText, completion));
                result = await responseMessage.Content.ReadAsStringAsync();
            }
            Answer answer = JsonConvert.DeserializeObject<Answer>(result);
            return answer;

        }



        private HttpRequestMessage BuildRequest(string requestText, CompletionOptions completion)
        {
            RequestBody requestBody = RequestBody.Create(
                _yandexGPTSettings.model_uri,
                completion,
                new Message[] { new Message() { text = requestText, role = "user" } }
                );


            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Headers = {
                    { _yandexGPTSettings.Authorization.Key, _yandexGPTSettings.Authorization.Value},
                    {_yandexGPTSettings.x_folder_id.Key,_yandexGPTSettings.x_folder_id.Value}
                      },
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody)),
                RequestUri = new Uri(_yandexGPTSettings.request_uri)
            };

            return httpRequestMessage;
        }



    }
}

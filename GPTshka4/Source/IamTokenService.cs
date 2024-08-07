using GPTshka4.Models.YandexGPTModels;
using Newtonsoft.Json;

namespace GPTshka4.Source
{
    public class IamTokenService : BackgroundService
    {
        private static System.Timers.Timer timer;
        private long interval = 3600000;
        private static readonly object synclock = new object();
        private static HttpClient http;
        private readonly Logger<IamTokenService> logger;

        public IamTokenService(Logger<IamTokenService> logger)
        {
          //  IamTokenContainer.getInstance(GetIamToken().Result);
            timer = new System.Timers.Timer();
            timer.Interval = interval;
            timer.AutoReset = true;
            StartIamTokenService();
            this.logger = logger;
        }

        private void StartIamTokenService()
        {
            timer.Elapsed += OnTimeEvent;
            timer.Start();
        }

        private static async void OnTimeEvent(Object sender, System.Timers.ElapsedEventArgs e)
        {
          //  IamTokenContainer.getInstance(await GetIamToken());
        }

        private static async Task<string> GetIamToken()
        {
            http = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://iam.api.cloud.yandex.net/iam/v1/tokens"),
                Content = new StringContent("{\"yandexPassportOauthToken\":\"y0_AgAAAAAs4RllAATuwQAAAAEKprKfAAB3t_0ebntF4LwnrJadyK69p3tngg\"}")
            };
            return JsonConvert.DeserializeObject<IamTokenResponseBody>
                ((await (await http.SendAsync(request)).Content.ReadAsStringAsync())).iamToken;

        }

        //protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //      //  logger.Log(LogLevel.Information, "IAM token requested, old token: ", IamTokenContainer.getInstance(null).IamToken);
        //        StartIamTokenService();
        //      //  logger.Log(LogLevel.Information, "IAM token requested, new token: ", IamTokenContainer.getInstance(null).IamToken);
        //    }
        //}
    }
}

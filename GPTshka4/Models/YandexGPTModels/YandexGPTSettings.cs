﻿namespace GPTshka4.Models.YandexGPTModels
{
    public class YandexGPTSettings
    {
      
        public KeyValuePair<string, string> Authorization { get; set; }
        public KeyValuePair<string, string> x_folder_id { get; set; }

        public string request_uri { get; set; }

        public string yandex_id { get; set; }

        public YandexGPTSettings(IConfiguration configuration)
        {
            Authorization = new KeyValuePair<string, string>("Authorization", configuration["Authorization"]);
            x_folder_id = new KeyValuePair<string, string>("x-folder-id", configuration["x-folder-id"]);
            request_uri = configuration["request-uri"];
            yandex_id = configuration["yandex-id"];

        }
    }
}

using apiTest.Models;

namespace apiTest
{
    public class RequestBody
    {
        public string modelUri { get; set; }

        public CompletionOptions completionOptions { get; set; }

        public Message[] messages { get; set; }

        public static RequestBody Create(string _modelUri, CompletionOptions _completionOptions, Message[] _messages)
        {
            return new RequestBody { completionOptions = _completionOptions, modelUri = _modelUri, messages = _messages };
        }
    }
}

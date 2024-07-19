using apiTest.Models;

namespace apiTest
{
    public class RequestBody
    {
        public string modelUri { get; set; }

        public CompletionOptions completionOptions { get; set; }

        public Message[] messages { get; set; }


    }
}

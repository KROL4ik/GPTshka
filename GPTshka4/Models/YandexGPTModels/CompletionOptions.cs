
namespace apiTest.Models
{
    public class CompletionOptions
    {
        public bool stream { get; set; }
        public float temperature { get; set; }
        public int maxTokens { get; set; }

        public static CompletionOptions Create (float _temperature,int _maxTokens)
        {
            return new CompletionOptions() { temperature = _temperature, maxTokens = _maxTokens, stream = false };
        }
    }
}

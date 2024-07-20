namespace GPTshka4.Source
{
    public class IamTokenContainer
    {
        private static IamTokenContainer instance;

        public string IamToken { get; set; }

        private IamTokenContainer(string iamToken)
        {
            IamToken = iamToken;
        }

        public static IamTokenContainer getInstance(string iamToken)
        {
            if (instance == null)
                instance = new IamTokenContainer(iamToken);
            instance.IamToken = iamToken;
            return instance;
        }
    }
}

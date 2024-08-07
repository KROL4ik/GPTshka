namespace GPTshka4.Source
{
    public class IamTokenContainer
    {
        private static IamTokenContainer instance;

        public string IamToken { get; set; }


        public static IamTokenContainer GetInstance()
        {
            if (instance == null)
                instance = new IamTokenContainer();
            return instance;
        }
    }
}

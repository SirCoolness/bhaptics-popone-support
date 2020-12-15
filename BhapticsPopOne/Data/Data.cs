namespace BhapticsPopOne.Data
{
    public class Data
    {
        public Players Players;

        public Data()
        {
            Players = new Players();
        }
        
        public void Initialize()
        {
            Players.Initialize();
        }
    }
}
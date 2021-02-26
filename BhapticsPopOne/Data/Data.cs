namespace BhapticsPopOne.Data
{
    public class Data
    {
        public Players Players;
        public static PhysicsSystem PhysicsSystem => PhysicsWrapper.System.Cast<PhysicsSystem>();

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
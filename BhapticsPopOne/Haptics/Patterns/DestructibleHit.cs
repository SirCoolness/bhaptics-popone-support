namespace BhapticsPopOne.Haptics.Patterns
{
    public class DestructibleHit
    {
        public static void Execute(Handedness hand)
        {
            string effectExtension = "";
            
            if (hand == Handedness.Left)
            {
                effectExtension = "_L";

            } else if (hand == Handedness.Right)
            {
                effectExtension = "_R";
            }
            
            Mod.Instance.Haptics.Player.SubmitRegistered($"Vest/DestructibleHit{effectExtension}");
            Mod.Instance.Haptics.Player.SubmitRegistered($"Arm/DestructibleHit{effectExtension}");
        }
    }
}
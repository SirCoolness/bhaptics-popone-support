namespace BhapticsPopOne.Haptics.Patterns
{
    public class HapticUtils
    {
        public static string HandExt(Handedness hand)
        {
            if (hand == Handedness.Left)
            {
                return "_L";

            } else if (hand == Handedness.Right)
            {
                return "_R";
            }

            return "";
        }
    }
}
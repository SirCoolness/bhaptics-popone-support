using UnityEngine;

namespace BhapticsPopOne
{
    public class ConfigHelpers
    {
        public static float EnforceIntensity(float intensity)
        {
            return Mathf.Clamp(intensity, 0f, 1f);
        }
    }
}
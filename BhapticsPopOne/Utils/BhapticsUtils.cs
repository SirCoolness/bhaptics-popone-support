using UnityEngine;

// lifted straight from https://github.com/bhaptics/haptic-library/blob/master/samples/unity-plugin/Assets/Bhaptics/SDK/Scripts/BhapticsUtils.cs
namespace BhapticsPopOne
{
    public class BhapticsUtils
    {
        public static float Angle(Vector3 fwd, Vector3 targetDir)
        {
            var fwd2d = new Vector3(fwd.x, 0, fwd.z);
            var targetDir2d = new Vector3(targetDir.x, 0, targetDir.z);

            float angle = Vector3.Angle(fwd2d, targetDir2d);

            if (AngleDir(fwd, targetDir, Vector3.up) == -1)
            {
                angle = 360.0f - angle;
                if (angle > 359.9999f)
                    angle -= 360.0f;
                return angle;
            }

            return angle;
        }

        private static int AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
        {
            Vector3 perp = Vector3.Cross(fwd, targetDir);
            float dir = Vector3.Dot(perp, up);

            if (dir > 0.0)
            {
                return 1;
            }

            if (dir < 0.0)
            {
                return -1;
            }

            return 0;
        }
    }
}
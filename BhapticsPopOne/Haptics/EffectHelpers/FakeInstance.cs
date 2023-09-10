using System;
using System.Collections.Generic;
using Bhaptics.Tact;

using PositionType = bHapticsLib.PositionID;
using DotPoint = bHapticsLib.DotPoint;
using PathPoint = bHapticsLib.PathPoint;
using RotationOption = bHapticsLib.RotationOption;
using ScaleOption = bHapticsLib.ScaleOption;

namespace BhapticsPopOne.Haptics
{
    public class FakeInstance : IHapticPlayer
    {
        public bool IsPlaying(string key)
        {
            return false;
        }

        public bool IsPlaying()
        {
            return false;
        }

        public void RegisterTactFileStr(string key, string tactFileStr)
        {
        }

        public void RegisterTactFileStrReflected(string key, string tactFileStr)
        {
        }

        public void Submit(string key, PositionType position, byte[] motorBytes, int durationMillis)
        {
        }

        public void Submit(string key, PositionType position, List<DotPoint> points, int durationMillis)
        {
        }

        public void Submit(string key, PositionType position, List<PathPoint> points, int durationMillis)
        {
        }

        public void Submit(string key, PositionType position, PathPoint point, int durationMillis)
        {
        }

        public void SubmitRegistered(string key, ScaleOption option)
        {
        }

        public void SubmitRegistered(string key, string altKey, ScaleOption option)
        {
        }

        public void SubmitRegistered(string key)
        {
        }

        public void SubmitRegistered(string key, int startTimeMillis)
        {
        }

        public void SubmitRegistered(string key, string altKey, ScaleOption option, RotationOption rOptions)
        {
        }

        public void TurnOff(string key)
        {
        }

        public void TurnOff()
        {
        }
    }
}
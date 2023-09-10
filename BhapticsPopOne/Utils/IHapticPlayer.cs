using System;
using System.Collections.Generic;
using PositionType = bHapticsLib.PositionID;
using DotPoint = bHapticsLib.DotPoint;
using PathPoint = bHapticsLib.PathPoint;
using RotationOption = bHapticsLib.RotationOption;
using ScaleOption = bHapticsLib.ScaleOption;

namespace Bhaptics.Tact
{
    public interface IHapticPlayer
    {
        bool IsPlaying(string key);
        bool IsPlaying();

        void RegisterTactFileStr(string key, string tactFileStr);
        void RegisterTactFileStrReflected(string key, string tactFileStr);

        void Submit(string key, PositionType position, byte[] motorBytes, int durationMillis);
        void Submit(string key, PositionType position, List<DotPoint> points, int durationMillis);
        void Submit(string key, PositionType position, List<PathPoint> points, int durationMillis);

        void SubmitRegistered(string key, ScaleOption option);
        void SubmitRegistered(string key, string altKey, ScaleOption option);
        void SubmitRegistered(string key, string altKey, ScaleOption option, RotationOption rOptions);

        void SubmitRegistered(string key);
        void SubmitRegistered(string key, int startTimeMillis);

        void TurnOff(string key);
        void TurnOff();
    }
}
using System;
using System.Collections.Generic;

using PositionType = MelonLoader.bHaptics.PositionType;
using DotPoint = MelonLoader.bHaptics.DotPoint;
using PathPoint = MelonLoader.bHaptics.PathPoint;
using RotationOption = MelonLoader.bHaptics.RotationOption;
using ScaleOption = MelonLoader.bHaptics.ScaleOption;

namespace Bhaptics.Tact
{
    public interface IHapticPlayer
    {
        bool IsPlaying(string key);
        bool IsPlaying();

        void RegisterTactFileStr(string key, string tactFileStr);
        void RegisterTactFileStrReflected(string key, string tactFileStr);

        void Submit(string key, PositionType position, byte[] motorBytes, int startTimeMillis);
        void Submit(string key, PositionType position, List<DotPoint> points, int startTimeMillis);
        void Submit(string key, PositionType position, List<PathPoint> points, int startTimeMillis);

        void SubmitRegistered(string key, ScaleOption option);
        void SubmitRegistered(string key, string altKey, ScaleOption option);
        void SubmitRegistered(string key, string altKey, ScaleOption option, RotationOption rOptions);

        void SubmitRegistered(string key);
        void SubmitRegistered(string key, int startTimeMillis);

        void TurnOff(string key);
        void TurnOff();
    }
}
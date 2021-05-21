using MelonLoader;
using System;
using System.Collections.Generic;

namespace BhapticsPopOne.Haptics
{
    class PlayerProxy : Bhaptics.Tact.IHapticPlayer
    {
        public bool IsPlaying(string key) => bHaptics.IsPlaying(key);

        public bool IsPlaying() => bHaptics.IsPlaying();

        public void RegisterTactFileStr(string key, string tactFileStr) => bHaptics.RegisterTactFileStr(key, tactFileStr);

        public void RegisterTactFileStrReflected(string key, string tactFileStr) => bHaptics.RegisterTactFileStrReflected(key, tactFileStr);

        public void Submit(string key, bHaptics.PositionType position, byte[] motorBytes, int startTimeMillis) => bHaptics.Submit(key, position, motorBytes, startTimeMillis);

        public void Submit(string key, bHaptics.PositionType position, List<bHaptics.DotPoint> points, int startTimeMillis) => bHaptics.Submit(key, position, points, startTimeMillis);

        public void Submit(string key, bHaptics.PositionType position, List<bHaptics.PathPoint> points, int startTimeMillis) => bHaptics.Submit(key, position, points, startTimeMillis);

        public void SubmitRegistered(string key, bHaptics.ScaleOption option) => bHaptics.SubmitRegistered(key, key, option);

        public void SubmitRegistered(string key, string altKey, bHaptics.ScaleOption option) => bHaptics.SubmitRegistered(key, altKey, option);

        public void SubmitRegistered(string key) => bHaptics.SubmitRegistered(key);

        public void SubmitRegistered(string key, int startTimeMillis) => bHaptics.SubmitRegistered(key, startTimeMillis);

        public void SubmitRegistered(string key, string altKey, bHaptics.ScaleOption option, bHaptics.RotationOption rOptions) => bHaptics.SubmitRegistered(key, altKey, option, rOptions);

        public void TurnOff(string key) => bHaptics.TurnOff(key);

        public void TurnOff() => bHaptics.TurnOff();
    }
}

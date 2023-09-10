using MelonLoader;
using System;
using System.Collections.Generic;
using System.Reflection;
using bHapticsLib;

using PositionType = bHapticsLib.PositionID;
using PathPoint = bHapticsLib.PathPoint;
using DotPoint = bHapticsLib.DotPoint;
using RotationOption = bHapticsLib.RotationOption;
using ScaleOption = bHapticsLib.ScaleOption;

namespace BhapticsPopOne.Haptics
{
    class PlayerProxy : Bhaptics.Tact.IHapticPlayer
    {
        public bool IsPlaying(string key) => bHapticsManager.IsPlaying(key);

        public bool IsPlaying() => bHapticsManager.IsPlayingAny();

        public void RegisterTactFileStr(string key, string tactFileStr) => bHapticsManager.RegisterPatternFromJson(key, tactFileStr);

        public void RegisterTactFileStrReflected(string key, string tactFileStr) => bHapticsManager.RegisterPatternSwappedFromJson(key, tactFileStr);


        public void Submit(string key, PositionType position, byte[] motorBytes, int durationMillis) => bHapticsManager.Play(key, durationMillis, position, motorBytes);

        public void Submit(string key, PositionType position, List<DotPoint> points, int durationMillis) => bHapticsManager.Play(key, durationMillis, position, points);

        public void Submit(string key, PositionType position, List<PathPoint> points, int durationMillis) => bHapticsManager.Play(key, durationMillis, position, points);

        public void SubmitRegistered(string key, ScaleOption option) => bHapticsManager.PlayRegistered(key, null, option);

        public void SubmitRegistered(string key, string altKey, ScaleOption option) => bHapticsManager.PlayRegistered(key, key == altKey ? null : altKey, option);

        public void SubmitRegistered(string key) => bHapticsManager.PlayRegistered(key);

        public void SubmitRegistered(string key, int startTimeMillis) => bHapticsManager.PlayRegistered(key, startTimeMillis);

        public void SubmitRegistered(string key, string altKey, ScaleOption option, RotationOption rOptions) => bHapticsManager.PlayRegistered(key, key == altKey ? null : altKey, option, rOptions);

        public void TurnOff(string key) => bHapticsManager.StopPlaying(key);

        public void TurnOff() => bHapticsManager.StopPlayingAll();
    }
}

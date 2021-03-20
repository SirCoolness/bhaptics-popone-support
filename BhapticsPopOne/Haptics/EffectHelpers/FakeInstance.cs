using System;
using System.Collections.Generic;
using Bhaptics.Tact;

namespace BhapticsPopOne.Haptics
{
    public class FakeInstance : IHapticPlayer
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Enable()
        {
            throw new NotImplementedException();
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public bool IsActive(PositionType type)
        {
            return false;
        }

        public bool IsPlaying(string key)
        {
            return false;
        }

        public bool IsPlaying()
        {
            return false;
        }

        public void Register(string key, Project project)
        {
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

        public void Submit(string key, PositionType position, DotPoint point, int durationMillis)
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

        public void SubmitRegisteredVestRotation(string key, RotationOption option)
        {
        }

        public void SubmitRegisteredVestRotation(string key, string altKey, RotationOption option)
        {
        }

        public void SubmitRegisteredVestRotation(string key, string altKey, RotationOption rOption, ScaleOption sOption)
        {
        }

        public void SubmitRegistered(string key)
        {
        }

        public void SubmitRegistered(string key, float duration)
        {
        }

        public void TurnOff(string key)
        {
        }

        public void TurnOff()
        {
        }

#pragma warning disable 169, 414, 67
        public event Action<PlayerResponse> StatusReceived;
    }
}
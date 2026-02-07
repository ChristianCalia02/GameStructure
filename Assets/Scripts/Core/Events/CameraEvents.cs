using System;
using UnityEngine;

namespace Core.Events
{
    public static class CameraEvents
    {
        public static Action<Transform> OnTargetChanged;
        public static Action<float> OnYaw;
        public static Action<float> OnPitch;
        public static Action<bool> OnToggleLook;
    }
}
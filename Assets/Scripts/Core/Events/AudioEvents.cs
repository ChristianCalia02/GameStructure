using System;
using UnityEngine;

namespace Core.Events
{
    public static class AudioEvents
    {
        public static Action<AudioClip, Vector3> OnPlayAtPosition;
        public static Action<AudioClip> OnPlayUI;
        public static Action<Vector3> OnFootstep;
    }
}
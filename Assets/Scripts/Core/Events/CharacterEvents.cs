using System;

namespace Core.Events
{
    public static class CharacterEvents
    {
        public static Action<UnityEngine.Vector3, float> OnMove;
        public static Action OnJump;
        public static Action<bool> OnSprint;

        public static Action OnClimbStarted;
        public static Action OnClimbStopped;
    }
}
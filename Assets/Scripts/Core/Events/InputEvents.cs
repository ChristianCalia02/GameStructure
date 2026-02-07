using System;

namespace Core.Events
{
    public static class InputEvents
    {
        public static Action<UnityEngine.Vector2> OnMoveInput;
        public static Action<UnityEngine.Vector2> OnLookInput;
        public static Action OnJumpPressed;
        public static Action<bool> OnSprintPressed;
        public static Action OnClimbPressed;
    }
}
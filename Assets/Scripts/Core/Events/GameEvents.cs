using System;
using UnityEngine;

namespace Core.Events { 
    public static class GameEvents 
    {
        //Input
        public static Action<Vector2> OnMoveInput;
        public static Action<Vector2> OnLookInput;

        //Character
        public static Action<Vector3, float> OnCharacterMove;
        public static Action<bool> OnCharacterSprint;
        public static Action OnCharacterJump;

        //Camera
        public static Action<Transform> OnCameraTargetChanged;
        public static Action<float> OnCameraYaw;
        public static Action<float> OnCameraPitch;

        // Audio
        public static Action<AudioClip, Vector3> OnPlaySoundAtPosition;
        public static Action<AudioClip> OnPlayUISound;
        //----SFX
        public static Action<Vector3> OnFootstep;
    }
}

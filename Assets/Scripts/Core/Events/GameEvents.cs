using System;
using UnityEngine;

namespace Core.Events { 
    public static class GameEvents 
    {
        //Input
        public static Action<Vector2> OnMoveInput;
        public static Action<Vector2> OnLookInput;

        //Character
        public static Action<Vector3> OnCharacterMove;

        //Camera
        public static Action<Transform> OnCameraTargetChanged;
        public static Action<float> OnCameraYaw;
        public static Action<float> OnCameraPitch;
    }
}

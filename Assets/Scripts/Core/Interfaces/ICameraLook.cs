using UnityEngine;

namespace Core.Interfaces
{
    public interface ICameraLook
    {
        void RotateYaw(float degrees);
        void RotatePitch(float degrees);
    }
}   
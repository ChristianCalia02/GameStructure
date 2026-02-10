using UnityEngine;

namespace Core.Interfaces
{
    public interface IClimbable
    {
        bool CanClimb(ICharacterContext character);
        void OnClimbStart(ICharacterContext character);
        void OnClimbStop(ICharacterContext character);

        Transform GetClimbTransform();
    }
}
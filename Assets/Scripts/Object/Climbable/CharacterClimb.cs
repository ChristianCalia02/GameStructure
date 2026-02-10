using Core.Events;
using Core.Interfaces;
using UnityEngine;

namespace Climb
{
    public class CharacterClimb : MonoBehaviour
    {
        public bool IsHanging { get; private set; }

        private IClimbable currentClimbable;

        public void SetAvailableClimbable(IClimbable climbable)
        {
            currentClimbable = climbable;
            UIEvents.OnClimbAvailable?.Invoke(true, climbable.GetClimbTransform().position);
        }

        public void ClearClimbable()
        {
            if (currentClimbable != null)
            {
                UIEvents.OnClimbAvailable?.Invoke(false, Vector3.zero);
                currentClimbable = null;
            }
        }

        public bool TryStartClimb(ICharacterContext character)
        {
            if (currentClimbable == null) return false;
            if (!currentClimbable.CanClimb(character)) return false;

            IsHanging = true;
            currentClimbable.OnClimbStart(character);
            return true;
        }

        public void StopClimb(ICharacterContext character)
        {
            if (!IsHanging) return;

            IsHanging = false;
            currentClimbable?.OnClimbStop(character);
        }

        public Transform GetCurrentClimbTransform() => currentClimbable?.GetClimbTransform();
    }
}
using UnityEngine;
namespace Core.Interfaces
{
    public interface ICharacterState
    {
        void Enter();
        void Exit();
        void Update();

        void HandleMove(Vector3 dir, float magnitude);
        void HandleSprint(bool sprint);
        void HandleJump();
    }
}
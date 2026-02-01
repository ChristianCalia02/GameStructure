using UnityEngine;
using Core.Events;

namespace Bootstrap { 
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform player;

        void Start()
        {
            GameEvents.OnCameraTargetChanged?.Invoke(player);
        }
    }
}
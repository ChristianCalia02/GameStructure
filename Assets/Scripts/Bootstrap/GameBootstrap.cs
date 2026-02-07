using UnityEngine;
using Core.Events;

namespace Bootstrap { 
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform player;

        void Start()
        {
            CameraEvents.OnTargetChanged?.Invoke(player);
        }
    }
}
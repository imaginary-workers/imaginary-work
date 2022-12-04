using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerChecker : MonoBehaviour
    {
        public event Action OnPlayerExit, OnPlayerEnter;
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            OnPlayerEnter?.Invoke();
        }
        void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            OnPlayerExit?.Invoke();
        }
    }
}
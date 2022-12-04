using System;
using UnityEngine;

namespace Game.Gameplay
{
    public class PlayerChecker : MonoBehaviour
    {
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

        public event Action OnPlayerExit, OnPlayerEnter;
    }
}
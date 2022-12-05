using Game.Dialogs;
using Game.Gameplay;
using Game.Gameplay.Interactable;
using UnityEngine;

namespace Game
{
    public class DestroyListenner : MonoBehaviour
    {
        [SerializeField] ObjectDamageable _damageable;
        [SerializeField] DialogEmitter _emitter;

        private void Awake()
        {
            _damageable.OnDeath += DeadHandler;
        }
        private void OnDestroy()
        {
            _damageable.OnDeath -= DeadHandler;
        }
        void DeadHandler(GameObject gm)
        {
            _emitter.Emit();
        }
    }
}

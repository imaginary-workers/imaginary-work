using Game.Dialogs;
using Game.Gameplay.Enemies;
using UnityEngine;

namespace Game.Tutorial
{
    public class NoEnemiesListener : MonoBehaviour
    {
        [SerializeField] DialogEmitter _dialogEmitter;
        private void Awake()
        {
            Enemy.OnNoEnemies += _dialogEmitter.Emit;
        }

        private void OnDestroy()
        {
            Enemy.OnNoEnemies -= _dialogEmitter.Emit;
        }
    }
}

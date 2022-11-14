using Game.Gameplay.Enemies;
using Game.Managers;
using UnityEngine;

namespace Game
{
    public class ToLevelOne : MonoBehaviour
    {
        [SerializeField] Enemy _enemy;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && Enemy.countEnemy <= 0)
            {
                GameManager.Instance.ConditionWin();
            }
        }
    }
}

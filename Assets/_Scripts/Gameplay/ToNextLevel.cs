using Game.Gameplay.Enemies;
using Game.Managers;
using Game.Config.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class ToNextLevel : MonoBehaviour
    {
        [SerializeField] SceneSO _nextScene;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && Enemy.countEnemy <= 0)
                GameManager.Instance.NextScene(_nextScene);

        }
    }
}

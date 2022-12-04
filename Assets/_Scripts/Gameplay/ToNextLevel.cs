using Game.Gameplay.Enemies;
using Game.Managers;
using Game.Scene.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class ToNextLevel : MonoBehaviour
    {
        [SerializeField] SceneSO _nextScene;

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && Enemy.countEnemy <= 0)
                GameManager.Instance.NextScene(_nextScene);
        }
    }
}
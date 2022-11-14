using Game.Managers;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class ToNextLevel : MonoBehaviour
    {
        [SerializeField] SceneSO _nextScene; 
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                GameManager.Instance.NextScene(_nextScene);
        }
    }
}

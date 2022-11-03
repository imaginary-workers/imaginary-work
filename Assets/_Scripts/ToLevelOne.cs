using Game.Managers;
using UnityEngine;

namespace Game
{
    public class ToLevelOne : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
                GameManager.Instance.ConditionWin();
        }
    }
}

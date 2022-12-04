using UnityEngine;

namespace Game.Gameplay
{
    public class AppearAgain : MonoBehaviour
    {
        [SerializeField] Transform _spawn;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) other.transform.position = _spawn.position;
        }
    }
}
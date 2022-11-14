using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AppearAgain : MonoBehaviour
    {
        [SerializeField] Transform _spawn;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.transform.position = _spawn.position; 
            }
        }
    }
}

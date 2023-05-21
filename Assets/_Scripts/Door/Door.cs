using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Door
{
    public class Door : MonoBehaviour
    {
        [SerializeField] Animator _ani;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _ani.SetTrigger("Open");
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _ani.SetTrigger("Opened");
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _ani.SetTrigger("Close");
            }
        }
    }
}

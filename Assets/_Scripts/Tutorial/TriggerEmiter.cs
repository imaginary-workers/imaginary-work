using Game.Dialogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Tutorial
{
    public class TriggerEmiter : MonoBehaviour
    {
        [SerializeField] DialogEmitter _emitter;
        [SerializeField] GameObject _trigger;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _emitter.Emit();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            Destroy(_trigger);
        }
    }
}

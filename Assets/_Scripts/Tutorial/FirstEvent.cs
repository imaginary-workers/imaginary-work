using Game.Dialogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class FirstEvent : MonoBehaviour
    {
        [SerializeField] DialogEmitter _emitter;
        void Start()
        {
            _emitter.Emit();
        }

    }
}

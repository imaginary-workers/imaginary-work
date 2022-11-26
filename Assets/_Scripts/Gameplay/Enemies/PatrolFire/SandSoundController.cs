using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SandSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audio;
        [SerializeField] AudioClip _attack;
        [SerializeField] AnimationEvent _sandAniEvent;

        private void Awake()
        {

        }
    }
}

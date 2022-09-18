using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParciclesController : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particle;

        private void Start()
        {
            TurnOff();
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
            _particle.Play();

        }

        public void TurnOff()
        {
            _particle.Stop();
            gameObject.SetActive(false);
        }
    }
}

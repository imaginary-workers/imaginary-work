using UnityEngine;

namespace Game
{
    public class ParticlesController : MonoBehaviour
    {
        [SerializeField] ParticleSystem _particle;
        [Range(1, 10)][SerializeField] float _delayOff;

        void Start()
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
            Invoke("GameObjectOff", _delayOff);
        }

        void GameObjectOff()
        {
            gameObject.SetActive(false);
        }

    }
}

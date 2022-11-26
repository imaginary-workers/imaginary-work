using Game.Gameplay;
using Game.Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SpecialHammer : MonoBehaviour
    {
        [SerializeField] PlayerController _controller;
        [SerializeField] MoveComponent _move;
        [SerializeField] SwayBehaviour _sway;
        Vector3 _offset;

        [SerializeField] GameObject _explosion;
        [SerializeField] Transform _pointOfExplosion;

        private void Start()
        {
            _offset = _sway.transform.position;
        }
        void StartSpecial()
        {
            _controller.enabled = false;
            _move.Velocity = Vector3.zero;
            _sway.transform.position = _controller.transform.position + _offset;
        }

        void SpawnExplosion()
        {
            var e = Instantiate(_explosion);
            e.transform.position = _pointOfExplosion.position;
            e.transform.forward = _controller.transform.forward;
            //CameraShake
        }

        void EndSpecial()
        {
            _controller.enabled = true;
            _sway.enabled = true;
        }
    }
}

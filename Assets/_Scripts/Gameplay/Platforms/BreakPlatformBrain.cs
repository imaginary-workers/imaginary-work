using System;
using UnityEngine;

namespace Game.Gameplay.Platforms
{
    public class BreakPlatformBrain : MonoBehaviour
    {
        [SerializeField] OnPlayerOver _onPlayerOver;
        [SerializeField] [Range(0f, 5f)] float _timeToBreakLayer = 5;
        [SerializeField] int _maxLayers = 3;
        [SerializeField] [Range(.1f, 10f)] int _timeToRespawn;
        [SerializeField] Collider _collider;
        float _time;

        public int MaxLayers => _maxLayers;
        public int CurrentLayer { get; private set; } = 3;

        void Awake()
        {
            CurrentLayer = _maxLayers;
        }

        void Update()
        {
            if (_onPlayerOver.IsPlayerOver && CurrentLayer != 0) _time += 1 * Time.deltaTime;

            if (_time >= _timeToBreakLayer && CurrentLayer > 0)
            {
                _time = 0;
                CurrentLayer--;
                OnChangeLayer?.Invoke(CurrentLayer);
                if (CurrentLayer <= 0)
                {
                    _collider.enabled = false;
                    _onPlayerOver.Reset();
                }
            }

            if (CurrentLayer == 0)
            {
                _time += 1 * Time.deltaTime;
                if (_time >= _timeToRespawn)
                {
                    CurrentLayer = _maxLayers;
                    _time = 0;
                    OnChangeLayer?.Invoke(CurrentLayer);
                    _collider.enabled = true;
                }
            }
        }

        public event Action<int> OnChangeLayer;
    }
}
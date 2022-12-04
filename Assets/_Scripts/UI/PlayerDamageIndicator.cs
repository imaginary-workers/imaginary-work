using System.Collections.Generic;
using Game.Gameplay;
using Game.Gameplay.Player;
using UnityEngine;

namespace Game.UI
{
    public class PlayerDamageIndicator : MonoBehaviour
    {
        [SerializeField] PlayerDamageable _damageable;
        [SerializeField] ObjectPooler _damageIngicatorPooler;
        [SerializeField] float _timeToDisappear = 3f;
        readonly List<SingleDamageIndicator> _indicators = new List<SingleDamageIndicator>();

        void OnEnable()
        {
            _damageable.OnTakeDamage += TakeDamageHandler;
        }

        void OnDisable()
        {
            _damageable.OnTakeDamage -= TakeDamageHandler;
        }

        void TakeDamageHandler(int damage, GameObject damaginGameObject)
        {
            var damageIndicator = _indicators.Find(indicator => indicator.Enemy == damaginGameObject);
            if (damageIndicator != null)
            {
                damageIndicator.Reuse(_timeToDisappear);
                return;
            }

            var indicator = _damageIngicatorPooler.GetPooledObject();
            indicator.SetActive(true);
            var singleDamageIndicator = indicator.GetComponent<SingleDamageIndicator>();
            singleDamageIndicator.Init(damaginGameObject, _damageable.gameObject, _timeToDisappear);
            _indicators.Add(singleDamageIndicator);
            singleDamageIndicator.OnDesactiveIndicator += Unregister;
        }

        void Unregister(SingleDamageIndicator indicator)
        {
            if (!_indicators.Contains(indicator)) return;
            indicator.OnDesactiveIndicator -= Unregister;
            _indicators.Remove(indicator);
        }
    }
}
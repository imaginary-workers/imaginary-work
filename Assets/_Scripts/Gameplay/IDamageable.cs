using System;
using Game.Gameplay.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IDamageable
    {
        public event Action<int, GameObject> OnTakeDamage, OnTakeStrongDamage;
        public void TakeDamage(int damage, ElementSO element, GameObject damaging);
        public event Action<GameObject> OnDeath;
    }
}
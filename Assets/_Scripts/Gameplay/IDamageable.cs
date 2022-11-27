using System;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public interface IDamageable
    {
        public event Action<int, GameObject> OnTakeDamage, OnTakeStrongDamage;
        public void TakeTamage(int damage, ElementSO element, GameObject damaging);
    }
}
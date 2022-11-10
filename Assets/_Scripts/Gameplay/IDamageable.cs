using System;
using Game.SO;

namespace Game.Gameplay
{
    public interface IDamageable
    {
        public event Action<int> OnTakeDamage, OnTakeStrongDamage;
        public void TakeTamage(int damage, ElementSO element);
    }
}
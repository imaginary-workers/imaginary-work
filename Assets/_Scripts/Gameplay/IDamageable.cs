using Game.SO;

namespace Game.Gameplay
{
    public interface IDamageable
    {
        public void TakeTamage(int damage, ElementSO element);
    }
}
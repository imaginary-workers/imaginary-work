using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] int _maxAmunicion = 0;
        [SerializeField] int _maxReserveAmunicion = 0;
        [SerializeField] AudioClip _reloadSound;
        [SerializeField] AudioClip _noHitSound;
        public int MaxAmunicion => _maxAmunicion;

        public AudioClip ReloadSound => _reloadSound;

        public AudioClip NoHitSound => _noHitSound;
        public int MaxReserveAmunicion => _maxReserveAmunicion;
    }
}
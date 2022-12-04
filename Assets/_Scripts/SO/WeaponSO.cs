using UnityEngine;

namespace Game.SO
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] int _maxAmunicion = 0;
        [SerializeField] int _maxReserveAmunicion = 0;
        [SerializeField] AudioClip _reloadSound;
        [SerializeField] AudioClip _reloadSoundStart;
        [SerializeField] AudioClip _noHitSound;
        [SerializeField] AudioClip _shootSound;
        public int MaxAmunicion => _maxAmunicion;

        public AudioClip ReloadSound => _reloadSound;
        public AudioClip ReloadSoundStart => _reloadSoundStart;

        public AudioClip NoHitSound => _noHitSound;
        public AudioClip ShootSound => _shootSound;
        public int MaxReserveAmunicion => _maxReserveAmunicion;
    }
}
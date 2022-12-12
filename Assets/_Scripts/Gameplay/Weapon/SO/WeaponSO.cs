using UnityEngine;

namespace Game.Gameplay.Weapons.SO
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Object/weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        [SerializeField] int _maxAmunicion;
        [SerializeField] int _maxReserveAmunicion;
        [SerializeField] AudioClip _reloadSound;
        [SerializeField] AudioClip _reloadSoundStart;
        [SerializeField] AudioClip _noHitSound;
        [SerializeField] AudioClip _shootSound;
        [SerializeField] int _energy;
        [SerializeField] int _maxEnergy;
        public int MaxAmunicion => _maxAmunicion;

        public AudioClip ReloadSound => _reloadSound;
        public AudioClip ReloadSoundStart => _reloadSoundStart;

        public AudioClip NoHitSound => _noHitSound;
        public AudioClip ShootSound => _shootSound;
        public int MaxReserveAmunicion => _maxReserveAmunicion;

        public int Energy { get => _energy; set => _energy = Mathf.Clamp(value,0,MaxEnergy); }
        public int MaxEnergy { get => _maxEnergy; }
    }
}
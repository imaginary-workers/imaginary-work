using UnityEngine;


namespace Game.Gameplay.Enemies.PatrolFire
{
    public class SandSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource _audio;
        [SerializeField] AudioClip _attack;
        [SerializeField] AnimatorController _aniController;


        private void Awake()
        {
           
        }
    }
}

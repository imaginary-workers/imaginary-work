using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Audio
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager _singleton;
        AudioSource _audioSource;

        void Awake()
        {
            if (_singleton != null && _singleton != this)
            {
                Destroy(gameObject);
            }
            else if (_singleton == null)
            {
                _singleton = this;
                DontDestroyOnLoad(gameObject);
                _audioSource = GetComponent<AudioSource>();
            }
        }
        
    }
}

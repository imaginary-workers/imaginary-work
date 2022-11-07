using System;
using UnityEngine;

namespace Game.Config
{
    [Serializable]
    public class AudioConfig
    {
        [SerializeField, Range(-80f, 20f)]
        float _master = 0f;

        [SerializeField, Range(-80f, 20f)]
        float _sound = 0f;

        [SerializeField, Range(-80f, 20f)]
        float _music = 0f;

        public AudioConfig(AudioConfig audioConfig)
        {
            Master = audioConfig.Master;
            Sound = audioConfig.Sound;           
            Music = audioConfig.Music;
           
        }

        public AudioConfig()
        {
        }

        public float Master
        {
            get => _master;
            set
            {
                if (value >= -80f && value <= 20f)
                {
                    _master = value;
                }
            }
        }

        public float Sound
        {
            get => _sound;
            set
            {
                if (value >= -80f && value <= 20f)
                {
                    _sound = value;
                }
            }
        }

        public float Music
        {
            get => _music;
            set
            {
                if (value >= -80f && value <= 20f)
                {
                    _music = value;
                }
            }
        }
    }
}

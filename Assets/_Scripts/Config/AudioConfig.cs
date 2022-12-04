using System;
using UnityEngine;

namespace Game.Config
{
    [Serializable]
    public sealed class AudioConfig : IAudioConfig
    {
        [SerializeField] [Range(-80f, 20f)] float _master;

        [SerializeField] [Range(-80f, 20f)] float _sound;

        [SerializeField] [Range(-80f, 20f)] float _music;

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
                if (value >= -80f && value <= 20f) _master = value;
            }
        }

        public float Sound
        {
            get => _sound;
            set
            {
                if (value >= -80f && value <= 20f) _sound = value;
            }
        }

        public float Music
        {
            get => _music;
            set
            {
                if (value >= -80f && value <= 20f) _music = value;
            }
        }
    }

    public interface IAudioConfig
    {
        public float Master { get; set; }
        public float Music { get; set; }
        public float Sound { get; set; }
    }
}
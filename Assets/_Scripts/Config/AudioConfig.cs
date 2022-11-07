using System;
using UnityEngine;

namespace Game.Config
{
    [Serializable]
    public sealed class AudioConfig : IAudioConfig
    {
        [SerializeField, Range(-80f, 20f)]
        float _master = 0f;

        [SerializeField, Range(-80f, 20f)]
        float _sound = 0f;

        [SerializeField, Range(-80f, 20f)]
        float _music = 0f;
        
        const float minRange = 0.0001f;
        const float maxRange = 1f;

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
        
        public float Master01
        {
            get => FromDbTo01(_master);
            set => _master = From01ToDb(value);
        }

        public float Sound01
        {
            get => FromDbTo01(_sound);
            set => _sound = From01ToDb(value);
        }

        public float Music01
        {
            get => FromDbTo01(_music);
            set => _music = From01ToDb(value);
        }

        float From01ToDb(float n)
        {
            float newValue;
            if (n < minRange)
            {
                newValue = minRange;
            }
            else if (n > maxRange)
            {
                newValue = maxRange;
            }
            else
            {
                newValue = n;
            }

            return Mathf.Log10(newValue) * 20;;
        }

        float FromDbTo01(float db)
        {
            float newValue;
            if (db < minRange)
            {
                newValue = minRange;
            }
            else if (db > maxRange)
            {
                newValue = maxRange;
            }
            else
            {
                newValue = db;
            }

            return Mathf.Pow(10, newValue / 20);
        }
    }

    public interface IAudioConfig
    {
        public float Master { get; set; }
        public float Music { get; set; }
        public float Sound { get; set; }
    }
}

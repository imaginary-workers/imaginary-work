using Game.Config;
using UnityEngine;

namespace Game.UI
{
    public class AudioConfigUIDecorator : IAudioConfig
    {
        const float minRange = 0.0001f;
        const float maxRange = 1f;
        readonly AudioConfig _wrapped;

        public AudioConfigUIDecorator(AudioConfig wrapped)
        {
            _wrapped = wrapped;
        }

        public float Master
        {
            get => FromDbTo01(_wrapped.Master);
            set => _wrapped.Master = From01ToDb(value);
        }

        public float Sound
        {
            get => FromDbTo01(_wrapped.Sound);
            set => _wrapped.Sound = From01ToDb(value);
        }

        public float Music
        {
            get => FromDbTo01(_wrapped.Music);
            set => _wrapped.Music = From01ToDb(value);
        }

        float From01ToDb(float n)
        {
            float newValue;
            if (n < minRange)
                newValue = minRange;
            else if (n > maxRange)
                newValue = maxRange;
            else
                newValue = n;

            return Mathf.Log10(newValue) * 20;
            ;
        }

        float FromDbTo01(float db)
        {
            var newValue = Mathf.Pow(10, db / 20);

            if (newValue < minRange)
                newValue = minRange;
            else if (newValue > maxRange)
                newValue = maxRange;

            return newValue;
        }
    }
}
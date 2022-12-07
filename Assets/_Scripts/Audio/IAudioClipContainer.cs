using UnityEngine;

namespace Game.Audio
{
    public interface IAudioClipContainer
    {
        public AudioClip AudioClip { get; }
        public bool PlayOnAwake { get; }
    }
}
﻿using UnityEngine;

namespace EZCameraShake
{
    public enum CameraShakeState
    {
        FadingIn,
        FadingOut,
        Sustained,
        Inactive
    }

    public class CameraShakeInstance
    {
        private Vector3 amt;

        /// <summary>
        ///     Should this shake be removed from the CameraShakeInstance list when not active?
        /// </summary>
        public bool DeleteOnInactive = true;

        private float fadeOutDuration, fadeInDuration;

        /// <summary>
        ///     The intensity of the shake. It is recommended that you use ScaleMagnitude to alter the magnitude of a shake.
        /// </summary>
        public float Magnitude;

        /// <summary>
        ///     How much influence this shake has over the local position axes of the camera.
        /// </summary>
        public Vector3 PositionInfluence;

        /// <summary>
        ///     How much influence this shake has over the local rotation axes of the camera.
        /// </summary>
        public Vector3 RotationInfluence;


        /// <summary>
        ///     Roughness of the shake. It is recommended that you use ScaleRoughness to alter the roughness of a shake.
        /// </summary>
        public float Roughness;

        private bool sustain;
        private float tick;

        /// <summary>
        ///     Will create a new instance that will shake once and fade over the given number of seconds.
        /// </summary>
        /// <param name="magnitude">The intensity of the shake.</param>
        /// <param name="fadeOutTime">How long, in seconds, to fade out the shake.</param>
        /// <param name="roughness">Roughness of the shake. Lower values are smoother, higher values are more jarring.</param>
        public CameraShakeInstance(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
        {
            Magnitude = magnitude;
            fadeOutDuration = fadeOutTime;
            fadeInDuration = fadeInTime;
            Roughness = roughness;
            if (fadeInTime > 0)
            {
                sustain = true;
                NormalizedFadeTime = 0;
            }
            else
            {
                sustain = false;
                NormalizedFadeTime = 1;
            }

            tick = Random.Range(-100, 100);
        }

        /// <summary>
        ///     Will create a new instance that will start a sustained shake.
        /// </summary>
        /// <param name="magnitude">The intensity of the shake.</param>
        /// <param name="roughness">Roughness of the shake. Lower values are smoother, higher values are more jarring.</param>
        public CameraShakeInstance(float magnitude, float roughness)
        {
            Magnitude = magnitude;
            Roughness = roughness;
            sustain = true;

            tick = Random.Range(-100, 100);
        }

        /// <summary>
        ///     Scales this shake's roughness while preserving the initial Roughness.
        /// </summary>
        public float ScaleRoughness { get; set; } = 1;

        /// <summary>
        ///     Scales this shake's magnitude while preserving the initial Magnitude.
        /// </summary>
        public float ScaleMagnitude { get; set; } = 1;

        /// <summary>
        ///     A normalized value (about 0 to about 1) that represents the current level of intensity.
        /// </summary>
        public float NormalizedFadeTime { get; private set; }

        private bool IsShaking => NormalizedFadeTime > 0 || sustain;

        private bool IsFadingOut => !sustain && NormalizedFadeTime > 0;

        private bool IsFadingIn => NormalizedFadeTime < 1 && sustain && fadeInDuration > 0;

        /// <summary>
        ///     Gets the current state of the shake.
        /// </summary>
        public CameraShakeState CurrentState
        {
            get
            {
                if (IsFadingIn)
                    return CameraShakeState.FadingIn;
                if (IsFadingOut)
                    return CameraShakeState.FadingOut;
                if (IsShaking)
                    return CameraShakeState.Sustained;
                return CameraShakeState.Inactive;
            }
        }

        public Vector3 UpdateShake()
        {
            amt.x = Mathf.PerlinNoise(tick, 0) - 0.5f;
            amt.y = Mathf.PerlinNoise(0, tick) - 0.5f;
            amt.z = Mathf.PerlinNoise(tick, tick) - 0.5f;

            if (fadeInDuration > 0 && sustain)
            {
                if (NormalizedFadeTime < 1)
                    NormalizedFadeTime += Time.deltaTime / fadeInDuration;
                else if (fadeOutDuration > 0)
                    sustain = false;
            }

            if (!sustain)
                NormalizedFadeTime -= Time.deltaTime / fadeOutDuration;

            if (sustain)
                tick += Time.deltaTime * Roughness * ScaleRoughness;
            else
                tick += Time.deltaTime * Roughness * ScaleRoughness * NormalizedFadeTime;

            return amt * Magnitude * ScaleMagnitude * NormalizedFadeTime;
        }

        /// <summary>
        ///     Starts a fade out over the given number of seconds.
        /// </summary>
        /// <param name="fadeOutTime">The duration, in seconds, of the fade out.</param>
        public void StartFadeOut(float fadeOutTime)
        {
            if (fadeOutTime == 0)
                NormalizedFadeTime = 0;

            fadeOutDuration = fadeOutTime;
            fadeInDuration = 0;
            sustain = false;
        }

        /// <summary>
        ///     Starts a fade in over the given number of seconds.
        /// </summary>
        /// <param name="fadeInTime">The duration, in seconds, of the fade in.</param>
        public void StartFadeIn(float fadeInTime)
        {
            if (fadeInTime == 0)
                NormalizedFadeTime = 1;

            fadeInDuration = fadeInTime;
            fadeOutDuration = 0;
            sustain = true;
        }
    }
}
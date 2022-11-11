using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public static class Utils
    {
        public static Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
        
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public static bool IsInRangeOfVision(
            Vector3 positionA, Vector3 positionB, float maxDistanceXZ, float maxDistanceY
            )
        {
            var distanceY = Math.Abs(positionB.y - positionA.y);
            positionB.y = positionA.y = 0;
            var distanceXZ = Vector3.Distance(positionB, positionA);

            return distanceXZ <= maxDistanceXZ && distanceY <= maxDistanceY;
        }
        
        public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        public static float pythagoreanTheorem(float a, float b)
        {
            return Mathf.Sqrt(a * a + b * b);
        }

        public static IEnumerator CO_HitStop(float seconds, float timeScale = 0f)
        {
            Time.timeScale = timeScale;
            yield return new WaitForSecondsRealtime(seconds);
            Time.timeScale = 1;
        }
    }
}
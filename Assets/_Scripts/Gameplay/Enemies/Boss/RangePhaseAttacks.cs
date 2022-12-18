using System;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    [Serializable]
    public class RangePhaseAttacks
    {
        [field: Header("Phase 1")]
        [field: SerializeField, Range(0f, 1f)]
        public float RangeAttack1 { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float RangeShoot1 { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float RangeCombo1 { get; private set; }
        [field: Header("Phase 2")]

        [field: SerializeField, Range(0f, 1f)]
        public float RangeAttack2 { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float RangeShoot2 { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float RangeCombo2 { get; private set; }
        [field: Header("Phase 3")]

        [field: SerializeField, Range(0f, 1f)]
        public float RangeAttack3 { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float RangeShoot3 { get; private set; }

        [field: SerializeField, Range(0f, 1f)]
        public float RangeCombo3 { get; private set; }

        public void RangePhaseAttackFilter()
        {
            RangeShoot1 = 1 - RangeAttack1 < RangeShoot1 ? 1 - RangeAttack1 : RangeShoot1;
            RangeCombo1 = 1 - RangeAttack1 - RangeShoot1 < RangeCombo1 ? 1 - RangeAttack1 - RangeShoot1 : RangeCombo1;
            RangeShoot2 = 1 - RangeAttack2 < RangeShoot2 ? 1 - RangeAttack2 : RangeShoot2;
            RangeCombo2 = 1 - RangeAttack2 - RangeShoot2 < RangeCombo2 ? 1 - RangeAttack2 - RangeShoot2 : RangeCombo2;
            RangeShoot3 = 1 - RangeAttack3 < RangeShoot3 ? 1 - RangeAttack3 : RangeShoot3;
            RangeCombo3 = 1 - RangeAttack3 - RangeShoot3 < RangeCombo3 ? 1 - RangeAttack3 - RangeShoot3 : RangeCombo3;
        }
    }
}
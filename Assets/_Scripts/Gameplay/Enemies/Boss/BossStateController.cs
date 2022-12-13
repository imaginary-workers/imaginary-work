using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class BossStateController : EnemyStateController
    {

        protected override void OnAwakeEnemy()
        {
            IdleState _idleState;
            AttackState _attackState;
            AttackComboState _attackComboState;
            AttackDistanceState _attackDistanceState;
            SpawnState _spawnState;
            WeakState _weakState;
            DeadState _deadState;
        }

        protected override void SetDeadState()
        {
            
        }
    }
}

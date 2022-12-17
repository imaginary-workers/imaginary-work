using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System;
using Game.Gameplay.Enemies.Kamikaze;

namespace Game.Gameplay.Enemies.Boss
{
    public class SpawnState : State
    {
        BossStateController _bossStateController;
        AnimatorController _animatorController;
        BossHealth _bossHealth;
        string _spawnIdleStartEvent;
        GameObject _enemySpawn;
        Transform _spawnTransform;
        float _timeMax;
        readonly int _rangeOfVisionOfKamikazes;

        bool _spawnReady;
        float _time;
        int _countEnemySpawn;
        int _spawnEnemies;

        public SpawnState(BossStateController bossStateController,
            AnimatorController animatorController,
            BossHealth bossHealth,
            string spawnIdleStartEvent,
            GameObject enemySpawn,
            Transform spawnTransform,
            float timeMax,
            int spawnEnemies,
            int rangeOfVisionOfKamikazes)
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _bossHealth = bossHealth;
            _spawnIdleStartEvent = spawnIdleStartEvent;
            _enemySpawn = enemySpawn;
            _spawnTransform = spawnTransform;
            _timeMax = timeMax;
            _spawnEnemies = spawnEnemies;
            _rangeOfVisionOfKamikazes = rangeOfVisionOfKamikazes;
        }

        public override void Enter()
        {
            _time = 0;
            _countEnemySpawn = 0;
            _animatorController.Spawn();
            _bossHealth.IsImmune = true;
            _animatorController.AddAnimationEvent(_spawnIdleStartEvent, SpawnStartHandler);
        }


        public override void Update()
        {
            _time += Time.deltaTime;

            if (!_spawnReady) return;
            if (_time >= _timeMax)
            {
                _time = 0;
                var enemy = Transform.Instantiate(_enemySpawn, _spawnTransform.transform.position, Quaternion.identity);
                var kamikazeStateController = enemy.GetComponent<KamikazeStateController>();
                if (kamikazeStateController != null)
                {
                    kamikazeStateController.RangeFollow = _rangeOfVisionOfKamikazes;
                }
                _countEnemySpawn++;
            }
            if(_countEnemySpawn >= _spawnEnemies)
            {
                _bossStateController.ChangeState(_bossStateController.IdleState);
            }
        }

        public override void Exit()
        {
            _bossHealth.IsImmune = false;
        }
        private void SpawnStartHandler()
        {
            _spawnReady = true;
        }
    }
}

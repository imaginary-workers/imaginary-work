using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using System;

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

        bool _spawnReady;
        float _time;
        int _countSpawn;
        int _spawnEnemis;

        public SpawnState(BossStateController bossStateController,
            AnimatorController animatorController,
            BossHealth bossHealth,
            string spawnIdleStartEvent,
            GameObject enemySpawn,
            Transform spawnTransform,
            float timeMax,
            int spawnEnemis)
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _bossHealth = bossHealth;
            _spawnIdleStartEvent = spawnIdleStartEvent;
            _enemySpawn = enemySpawn;
            _spawnTransform = spawnTransform;
            _timeMax = timeMax;
            _spawnEnemis = spawnEnemis;
        }

        public override void Enter()
        {
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
                Transform.Instantiate(_enemySpawn, _spawnTransform.transform.position, Quaternion.identity);
                _countSpawn++;
            }
            if(_countSpawn >= _spawnEnemis)
            {
                _bossStateController.ChangeState(_bossStateController.IdleState);
            }
        }

        public override void Exit()
        {

        }
        private void SpawnStartHandler()
        {
            _spawnReady = true;
        }
    }
}

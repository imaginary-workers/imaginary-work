using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class IdleState : State
    {
        readonly BossStateController _bossStateController;
        private readonly AnimatorController _animatorController;
        float _speed;
        private Vector3 _position;
        readonly Transform _target;
        readonly float _minAttackTime;
        readonly float _maxAttackTime;
        readonly Transform _transform;
        float _time;
        float _timeMax;
        private float _rangeAttack;
        private float _rangeShoot;
        private float _rangeCombo;
        int _Fase;
        BossHealth _bossHealt;

        public IdleState(BossStateController bossStateController, AnimatorController animatorController, float speed, Transform target, float minAttackTime, float maxAttackTime, BossHealth bossHealt)
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _speed = speed;
            _target = target;
            _minAttackTime = minAttackTime;
            _maxAttackTime = maxAttackTime;
            _transform = _bossStateController.transform;
            _bossHealt = bossHealt;
        }


        public override void Enter()
        {
            Updatephase();
            _animatorController.Idle();
            _time = 0;
            _position = _transform.position;
            _timeMax = Random.Range(_minAttackTime, _maxAttackTime);
        }

        private void Updatephase()
        {
            switch (_bossHealt.CurrentPhase)
            {
                case 2:
                    _rangeAttack = 0.5f;
                    _rangeShoot = 0.5f;
                    _rangeCombo = 0;
                    break;
                case 3:
                    _rangeAttack = 0.33f;
                    _rangeShoot = 0.33f;
                    _rangeCombo = 0.33f;
                    break;
                default:
                    _rangeAttack = 1;
                    _rangeShoot = 0;
                    _rangeCombo = 0;
                    break;
            }
        }

        public override void Update()
        {
            if (Enemy.CountEnemy > 1) return;
            _time += Time.deltaTime;
            if (_time >= _timeMax)
            {
                var random = Random.Range(0f, 1f);
                if (random >= 0 && random <= _rangeAttack)
                {
                    _bossStateController.ChangeState(_bossStateController.AttackState);
                }
                else if (random > _rangeAttack && random <= _rangeShoot + _rangeAttack)
                {
                    _bossStateController.ChangeState(_bossStateController.AttackDistanceState);
                }
                else
                {
                    _bossStateController.ChangeState(_bossStateController.AttackComboState);
                }
                return;
            }
            var targetPosition = _target.position;
            targetPosition.y = _transform.position.y;
            targetPosition.z = _transform.position.z;
            var position = Vector3.Slerp(_transform.position, targetPosition, _speed * Time.deltaTime);
            position.z = _position.z;
            position.y = _position.y;
            _transform.position = position;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}

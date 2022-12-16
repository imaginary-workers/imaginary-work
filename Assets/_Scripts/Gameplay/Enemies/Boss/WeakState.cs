using UnityEngine;

namespace Game.Gameplay.Enemies.Boss
{
    public class WeakState : State
    {
        readonly BossStateController _bossStateController;
        readonly AnimatorController _animatorController;
        readonly BossHealth _bossHealth;
        float _time;
        readonly float _waitToStaggerFinished;

        public WeakState(BossStateController bossStateController, AnimatorController animatorController, BossHealth bossHealth, float waitToStaggerFinished)
        {
            _bossStateController = bossStateController;
            _animatorController = animatorController;
            _bossHealth = bossHealth;
            _waitToStaggerFinished = waitToStaggerFinished;
        }


        public override void Enter()
        {
            Debug.Log("Weak");
            _time = 0;
            _bossHealth.IsImmune = true;
            _bossHealth.OnTakeAnyDamage += ChangeToNextState;
            _animatorController.Weak();
        }

        void ChangeToNextState()
        {
            _bossStateController.ChangeState(_bossStateController.IdleState);
        }

        public override void Update()
        {
            _time += Time.deltaTime;
            if (_time >= _waitToStaggerFinished)
            {
                _bossHealth.IsImmune = false;
            }
        }

        public override void Exit()
        {
            Debug.Log("Exit Weak");
            _animatorController.Idle();
        }
    }
}

﻿using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class EnemyStateController : Enemy
    {
        State _currentState;
        EnemyDamageable _damageable;
        State _lastState;
        protected State deadState;

        protected EnemyDamageable Damageable
        {
            get
            {
                if (_damageable == null)
                    _damageable = GetComponent<EnemyDamageable>();
                return _damageable;
            }
        }

        void Start()
        {
            SetDeadState();
            Damageable.OnDeath += ChangeToDeathState;
        }

        void Update()
        {
            _currentState.Update();
        }

        public void ChangeState(State nextState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
                _lastState = _currentState;
            }

            _currentState = nextState;
            _currentState.Enter();
        }

        public void ChangeBackToLastState()
        {
            if (_lastState == null) return;
            _currentState.Exit();
            _currentState = _lastState;
            _lastState = null;
            _currentState.Enter();
        }

        public virtual void DestroyGameObject(float seconds = 0)
        {
            Damageable.OnDeath -= ChangeToDeathState;
            Destroy(gameObject, seconds);
        }

        void ChangeToDeathState(GameObject damaging)
        {
            if (deadState == null) return;
            ChangeState(deadState);
        }

        protected abstract void SetDeadState();
    }
}
using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public abstract class EnemyStateController: Enemy
    {
        State _currentState;
        State _lastState;
        [SerializeField] EnemyDamageable _damageable;
        protected State deadState;

        void Start()
        {
            _damageable.OnDeath += ChangeToDeathState;
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

        public virtual void DestroyGameObject()
        {
            Destroy(gameObject);
            _damageable.OnDeath -= ChangeToDeathState;
        }

        void ChangeToDeathState()
        {
            if (deadState == null) return;
            ChangeState(deadState);
        }
    }
}
namespace Game.Gameplay.Enemies
{
    public abstract class EnemyStateController: Enemy
    {
        State _currentState;
        State _lastState;
        EnemyDamageable _damageable;
        protected State deadState;

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

        public virtual void DestroyGameObject()
        {
            Damageable.OnDeath -= ChangeToDeathState;
            Destroy(gameObject);
        }

        void ChangeToDeathState()
        {
            if (deadState == null) return;
            ChangeState(deadState);
        }

        protected EnemyDamageable Damageable
        {
            get
            {
                if (_damageable == null)
                    _damageable = GetComponent<EnemyDamageable>();
                return _damageable;
            }
        }

        protected abstract void SetDeadState();
    }
}
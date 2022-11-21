using UnityEngine;

namespace Game.Gameplay
{
    public class LiftStart : Lift
    {
        [SerializeField] PlayerChecker _checker;

        void Awake()
        {
            _checker.OnPlayerExit += CloseDoors;
        }
        void OnDestroy()
        {
            _checker.OnPlayerExit -= CloseDoors;
        }
        void Start()
        {
            StartGame();
        }
    }
}

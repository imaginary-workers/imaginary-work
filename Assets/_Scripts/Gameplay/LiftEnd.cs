using Game.Gameplay.Enemies;
using Game.Managers;
using Game.SO;
using UnityEngine;

namespace Game.Gameplay
{
    public class LiftEnd : Lift
    {
        [SerializeField] PlayerChecker _checker;
        [SerializeField] SceneSO _nextScene;

        void Start()
        {
            Enemy.OnNoEnemies += OpenDoors;
            _checker.OnPlayerEnter += EndGame;
        }

        void OnDestroy()
        {
            Enemy.OnNoEnemies -= OpenDoors;
            _checker.OnPlayerEnter -= EndGame;
        }

        public override void EndGame()
        {
            base.EndGame();
            GameManager.Instance.NextScene(_nextScene);
        }
    }
}
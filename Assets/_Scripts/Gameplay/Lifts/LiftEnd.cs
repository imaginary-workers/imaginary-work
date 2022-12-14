using Game.Gameplay.Enemies;
using Game.Managers;
using Game.Scene.SO;
using UnityEngine;

namespace Game.Gameplay.Lifts
{
    public class LiftEnd : MonoBehaviour
    {
        [SerializeField] PlayerChecker _checker;
        [SerializeField] SceneSO _nextScene;
        [SerializeField] LiftAnimations _lift;
        [SerializeField] LiftDoorAnimations _liftDoor;


        void Start()
        {
            Enemy.OnNoEnemies += _liftDoor.OpenDoors;
            _checker.OnPlayerEnter += CloseDoors;

            _liftDoor.OnClosed += EndGame;
        }

        void OnDestroy()
        {
            Enemy.OnNoEnemies -= _liftDoor.OpenDoors;
            _checker.OnPlayerEnter -= _liftDoor.CloseDoors;
            _liftDoor.OnClosed -= EndGame;
        }

        void CloseDoors()
        {
            PlayManager.Instance.SetPlayerControlActive(false, true);
            _liftDoor.CloseDoors();
        }

        void EndGame()
        {
            _lift.Depart();
            GameManager.Instance.NextScene(_nextScene);
        }
    }
}
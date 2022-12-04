using UnityEngine;

namespace Game.Gameplay.Lifts
{
    public class LiftStart : MonoBehaviour
    {
        [SerializeField] PlayerChecker _checker;
        [SerializeField] Transform _liftTransform;
        [SerializeField] LiftAnimations _lift;
        [SerializeField] LiftDoorAnimations _liftDoor;

        public LiftAnimations Lift => _lift;

        void Awake()
        {
            _checker.OnPlayerExit += _liftDoor.CloseDoors;
            _lift.OnUpFinished += _liftDoor.OpenDoors;
        }

        public void Start()
        {
            _lift.Arrive();
        }

        void OnDestroy()
        {
            _checker.OnPlayerExit -= _liftDoor.CloseDoors;
            _lift.OnUpFinished -= _liftDoor.OpenDoors;
        }

        public void PlacePlayer(GameObject player)
        {
            player.transform.position = _liftTransform.position;
            player.transform.forward = _liftTransform.forward * -1;
        }
    }
}
using UnityEngine;

namespace Game.Gameplay
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        
        public void StartGame()
        {
            _animator.SetTrigger("UpAndOpen");
        }
        
        public virtual void EndGame()
        {
            _animator.SetTrigger("CloseAndUp");
        }

        public void CloseDoors()
        {
            _animator.SetTrigger("CloseDoors");
        }

        public void OpenDoors()
        {
            _animator.SetTrigger("OpenDoors");
        }
    }
}
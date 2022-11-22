using UnityEngine;

namespace Game.Gameplay
{
    public class Lift : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        
        [ContextMenu("StartGame")]
        public void StartGame()
        {
            _animator.SetTrigger("UpAndOpen");
        }
        [ContextMenu("EndGame")]
        public virtual void EndGame()
        {
            _animator.SetTrigger("CloseAndUp");
        }
        [ContextMenu("CloseDoors")]
        public void CloseDoors()
        {
            _animator.SetTrigger("CloseDoors");
        }
        [ContextMenu("OpenDoors")]
        public void OpenDoors()
        {
            _animator.SetTrigger("OpenDoors");
        }
    }
}
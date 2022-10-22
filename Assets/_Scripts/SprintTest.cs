
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class SprintTest : MonoBehaviour
    {
        [SerializeField] Animator _aminator;


        public void Sprint(InputAction.CallbackContext context)
        {
            if (context.started)
                _aminator.SetBool("IsSprinting", true);
            else if (context.canceled)
                _aminator.SetBool("IsSprinting", false);
        }
    }
}

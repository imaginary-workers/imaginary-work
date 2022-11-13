using UnityEngine;

namespace Game
{
    public class RagdollTest : MonoBehaviour
    {
        [SerializeField] Collider[] _colliders;
        [SerializeField] Rigidbody[] _rbs;


        // Start is called before the first frame update
        void Start()
        {
            SetRagdoll(false);
        }

        public void SetRagdoll(bool enable)
        {
            foreach (Rigidbody rb in _rbs)
            {
                rb.isKinematic = !enable;
                rb.useGravity = enable;
            }

            foreach (Collider collider in _colliders)
            {
                collider.enabled = enable;
            }
        }
    }
}

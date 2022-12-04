using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] GameObject _explosion;

        private void OnTriggerEnter(Collider other)
        {
            var e = Instantiate(_explosion);
            e.transform.position = transform.position;
            e.transform.localScale = new Vector3(10, 10, 10);
        }
    }
}

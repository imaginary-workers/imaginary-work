using UnityEngine;

namespace Game.Gameplay.Enemies
{
    public class SpawnDrops : MonoBehaviour
    {
        [SerializeField] GameObject _drop1;
        [SerializeField] GameObject _drop2;
        [SerializeField] [Range(0f, 1f)] float _chancePercentage;

        public void Drop()
        {
            if (Random.Range(0f, 1f) <= _chancePercentage)
                Instantiate(_drop1, transform.position, Quaternion.identity);
            else
                Instantiate(_drop2, transform.position, Quaternion.identity);
        }
    }
}
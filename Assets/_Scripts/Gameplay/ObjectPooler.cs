using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class ObjectPooler : MonoBehaviour
    {
        [Header("Dependencies")] [SerializeField]
        GameObject _pooledObject;

        [SerializeField] int _pooledAmount = 1;
        [SerializeField] bool _willGrow = true;
        [SerializeField] List<GameObject> _pooledObjects;
        [SerializeField] Transform _parent;

        void Start()
        {
            _pooledObjects = new List<GameObject>();
            for (var i = 0; i < _pooledAmount; i++) CreateObject();
        }

        public GameObject GetPooledObject()
        {
            for (var i = 0; i < _pooledObjects.Count; i++)
                if (!_pooledObjects[i].activeSelf)
                    return _pooledObjects[i];

            if (_willGrow) return CreateObject();

            return null;
        }

        GameObject CreateObject()
        {
            GameObject obj;
            if (_parent != null)
                obj = Instantiate(_pooledObject, _parent);
            else
                obj = Instantiate(_pooledObject);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
            return obj;
        }
    }
}
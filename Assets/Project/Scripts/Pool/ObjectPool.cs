using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        private List<GameObject> _pooledObjects;
        [SerializeField] private GameObject objectToPool;
        [SerializeField] private int amountToPool;
        [SerializeField] private bool grow;
        

        private void Start()
        {
            _pooledObjects = new List<GameObject>();
            for(int i = 0; i < amountToPool; i++)
            {
                CreateNewPooledObject();
            }
        }
        
        public GameObject GetPooledObject()
        {
            foreach (var t in _pooledObjects)
            {
                if(!t.activeInHierarchy)
                {
                    return t;
                }
            }

            return grow ? CreateNewPooledObject() : null;
        }

        private GameObject CreateNewPooledObject()
        {
            var tmp = Instantiate(objectToPool, transform);
            tmp.SetActive(false);
            _pooledObjects.Add(tmp);

            return tmp;
        }
    }
}
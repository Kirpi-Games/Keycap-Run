using System.Collections.Generic;
using Akali.Common;
using UnityEngine;

namespace Akali.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Pool", menuName = App.AppName + "/Pools/Pool")]
    public class PoolScriptableObject : ScriptableObject
    {
        public GameObject poolPrefab;
        public int poolCount;
        
        public void CreatePool(Queue<GameObject> queue, Transform poolParent)
        {   
            for (var i = 0; i < poolCount; i++)
            {
                var poolObject = Instantiate(poolPrefab, poolParent);
                poolObject.SetActive(false);
                queue.Enqueue(poolObject);
            }
        }

        public GameObject Dequeue(Queue<GameObject> pool)
        {
            var gameObject = pool.Dequeue();
            gameObject.SetActive(true);
            return gameObject;
        }

        public void Enqueue(GameObject gameObject, Queue<GameObject> pool, Transform poolParent)
        {
            var transform = poolParent.transform;
            gameObject.SetActive(false);
            gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
            pool.Enqueue(gameObject);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private float spawnInternal = .5f;
    [SerializeField] private ObjectPool _objectPool = null;
    
    [Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }

    [SerializeField] private Pool[] pools = null;

    private void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>();

            for (int i = 0; i <  pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate( pools[j].objectPrefab);
                obj.SetActive(false);
            
                pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    public GameObject GetPooledObject(int objectType)
    {
        if (objectType >= pools.Length)
        {
            return null;
        }
        
        GameObject obj =  pools[objectType].pooledObjects.Dequeue();
        
        obj.SetActive(true);
        
        pools[objectType].pooledObjects.Enqueue(obj);

        return obj;
    }
    
    private IEnumerator SpawnRoutine() // Dost ve düşman objelerini oluşması
    {
        int counter = 0;
        while (true)
        {
            GameObject obj =_objectPool.GetPooledObject(counter++%2);
            if (counter == 0)
            {
                obj.transform.position = new Vector3(1f, 0f, -3f);
            }
            else if(counter == 1)
            {
                obj.transform.position = new Vector3(-1f, 0f, 100f);

            }
            yield return new WaitForSeconds(spawnInternal);
        }
    }

 
}

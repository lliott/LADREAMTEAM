using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static LemmingPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("tag " + tag + " doesnt exist");
            return null;
        }

        Queue<GameObject> objectPool = poolDictionary[tag];

        bool allActive = true;
        foreach (GameObject pooledObject in objectPool)
        {
            if (!pooledObject.activeSelf)
            {
                allActive = false;
                break;
            }
        }

        if (allActive)
        {
            //waiting for available lemmy :^)
            return null;
        }

        GameObject objectToSpawn = objectPool.Dequeue();

        while (objectToSpawn.activeSelf)
        {
            objectPool.Enqueue(objectToSpawn);
            objectToSpawn = objectPool.Dequeue();
        }

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectPool.Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReuseLemming(GameObject lemming)
    {
        lemming.SetActive(false);
    }
}

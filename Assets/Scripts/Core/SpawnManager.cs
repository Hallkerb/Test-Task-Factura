using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [System.Serializable]
    public struct PoolItem
    {
        public SpawnableType type;
        public GameObject prefab;
    }

    [SerializeField] private List<PoolItem> poolItems;

    private Dictionary<SpawnableType, ISpawnable> prefabs = new();
    private Dictionary<SpawnableType, Stack<ISpawnable>> pool = new();

    private Dictionary<SpawnableType, List<ISpawnable>> instantiatedObjects = new();

    private void Awake()
    {
        Instance = this;
        
        foreach (var item in poolItems)
        {
            if (item.prefab != null && item.prefab.TryGetComponent(out ISpawnable spawnable))
            {
                prefabs[item.type] = spawnable;
                pool[item.type] = new Stack<ISpawnable>();
            }
        }
    }

    public GameObject Spawn(SpawnableType type, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        ISpawnable spawnable;
        GameObject obj;

        if (pool[type].TryPop(out ISpawnable pooledObj) && pooledObj != null)
        {
            spawnable = pooledObj;
            obj = pooledObj.GameObject;
            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);
        }
        else
        {
            spawnable = prefabs[type];
            obj = Instantiate(spawnable.GameObject, position, rotation, parent);
            
            if (obj.TryGetComponent(out SpawnableObject spawnableObject))
            {
                spawnable = spawnableObject;

                if (instantiatedObjects.ContainsKey(type) == false)
                    instantiatedObjects.Add(type, new List<ISpawnable>());
                
                instantiatedObjects[type].Add(spawnable);
            }
        }
        
        spawnable.OnDespawn += Despawn;

        if (obj.TryGetComponent(out IHealth health))
            UIHealthPool.Instance.Get(spawnable, health, obj.transform);

        return obj;
    }

    public void Despawn(SpawnableType type, ISpawnable obj)
    {
        obj.OnDespawn -= Despawn;
        obj.GameObject.SetActive(false);

        if (pool.ContainsKey(type) == false)
        {
            pool[type] = new Stack<ISpawnable>();
        }

        pool[type].Push(obj);
    }

    public void DespawnAll()
    {
        foreach(var pair in instantiatedObjects)
        {
            for (int i = pair.Value.Count - 1; i >= 0; i--)
            {
                pair.Value[i].Despawn();
            }
        }
    }
}

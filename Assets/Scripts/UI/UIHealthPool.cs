using System.Collections.Generic;
using UnityEngine;

public class UIHealthPool : MonoBehaviour
{
    public static UIHealthPool Instance { get; private set; }

    [SerializeField] private UIHealth uiHealthPrefab;

    private Stack<UIHealth> pool = new Stack<UIHealth>();

    private void Awake()
    {
        Instance = this;
    }

    public UIHealth Get(ISpawnable spawnable, IHealth health, Transform healthTransform)
    {
        UIHealth uiHealth;

        if (pool.TryPop(out UIHealth pooledObj) && pooledObj != null)
        {
            uiHealth = pooledObj;
            uiHealth.gameObject.SetActive(true);
        }
        else
        {
            uiHealth = Instantiate(uiHealthPrefab, transform);
        }

        uiHealth.Initialize(health, healthTransform);

        spawnable.OnDespawn += OnDespawnHandler;

        void OnDespawnHandler(SpawnableType type, ISpawnable spawnable)
        {
            spawnable.OnDespawn -= OnDespawnHandler;
            ReturnToPool(uiHealth);
        }

        return uiHealth;
    }

    private void ReturnToPool(UIHealth uiHealth)
    {
        uiHealth.gameObject.SetActive(false);
        pool.Push(uiHealth);
    }
}

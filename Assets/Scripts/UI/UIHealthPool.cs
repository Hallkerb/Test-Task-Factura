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

    public UIHealth Get(IHealth health, Transform healthTransform)
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

        health.OnDied += OnUnitDied;

        void OnUnitDied(IHealth deadHealth)
        {
            health.OnDied -= OnUnitDied;
            ReturnToPool(uiHealth);
        }

        return uiHealth;
    }

    public void ReturnToPool(UIHealth uiHealth)
    {
        uiHealth.gameObject.SetActive(false);
        pool.Push(uiHealth);
    }
}

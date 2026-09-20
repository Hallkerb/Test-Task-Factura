using System;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour, ISpawnable
{
    [SerializeField] private SpawnableType spawnableType;

    protected bool isDespawned = false;

    public SpawnableType SpawnableType => spawnableType;

    public GameObject GameObject => gameObject;

    public event Action<SpawnableType, ISpawnable> OnDespawn;

    protected virtual void OnEnable()
    {
        ResetState();
    }

    public virtual void Despawn()
    {
        if (isDespawned) return;
        
        isDespawned = true;
        OnDespawn?.Invoke(SpawnableType, this);
    }

    protected virtual void ResetState()
    {
        isDespawned = false;
    }
}

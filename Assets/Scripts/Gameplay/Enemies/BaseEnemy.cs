using System;
using UnityEngine;

public class BaseEnemy : SpawnableObject
{
    protected IHealth health;

    [SerializeField] private float damage = 10;

    protected virtual void Awake()
    {
        health = GetComponent<IHealth>();

        health.OnDied += (IHealth health) => Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth target) && target.Team == Team.Player && isDespawned == false)
        {
            target.TakeDamage(damage);
            health.TakeDamage(health.MaxHP);
        }
    }

    protected override void ResetState()
    {
        base.ResetState();

        health.ResetState();
    }
}

using System;
using UnityEngine;

public class BaseEnemy : SpawnableObject
{
    private IHealth health;

    [SerializeField] private float damage = 10;

    private void Awake()
    {
        health = GetComponent<IHealth>();

        health.OnDied += (IHealth health) => Despawn();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        ResetState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth target) && target.Team == Team.Player)
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

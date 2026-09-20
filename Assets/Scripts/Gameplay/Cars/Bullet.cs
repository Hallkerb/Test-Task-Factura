using System;
using UnityEngine;

public class Bullet : SpawnableObject, ISpawnable
{
    [SerializeField] private Team targetTeam;

    [SerializeField] private float speed = 10;
    [SerializeField] private float damage = 2;
    [SerializeField] private float maxRange = 100;
    
    private float traveledDistance;


    protected override void OnEnable()
    {
        traveledDistance = 0f;
        
        base.OnEnable();
    }

    private void Update()
    {
        if (traveledDistance < maxRange)
            Move();
        else
            Despawn();
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        traveledDistance += step;

        transform.position += transform.up * step;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth target) && target.Team == targetTeam)
        {
            target.TakeDamage(damage);
            Despawn();
        }
    }
}

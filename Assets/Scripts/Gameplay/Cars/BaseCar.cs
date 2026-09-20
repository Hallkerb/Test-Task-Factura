using System;
using UnityEngine;

public abstract class BaseCar : SpawnableObject
{
    [SerializeField] private CarMovement movement;
    [SerializeField] private CarTurret turret;
    [SerializeField] private Health health;

    private void Start()
    {
        health.OnDied += (IHealth health) => Despawn();

        GameManager manager = GameManager.Instance;
        manager.OnStart += StartRace;
        manager.OnEnd += StopRace;
    }

    private void StartRace()
    {
        movement.StartMove();
        turret.StartAim();
    }

    private void StopRace()
    {
        movement.StopMove();
        turret.StopAim();
    }

    protected override void ResetState()
    {
        base.ResetState();

        movement.ResetState(Vector3.zero);
        turret.ResetState();
        health.ResetState();
    }

    public override void Despawn()
    {
        StopRace();

        base.Despawn();
    }
}
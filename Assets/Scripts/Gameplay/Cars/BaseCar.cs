using System;
using UnityEngine;

public abstract class BaseCar : SpawnableObject
{
    [SerializeField] private CarMovement movement;
    [SerializeField] private CarTurret turret;
    [SerializeField] private Health health;
    [SerializeField] private HitFeedback hitFeedback;
    [SerializeField] private CarEffectsController effectsController;

    protected override void OnEnable()
    {
        base.OnEnable();

        if (health != null)
        {
            health.OnDamageTaken += OnDamageTaken;
        }
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnDamageTaken -= OnDamageTaken;
        }
    }

    private void Start()
    {
        health.OnDied += (IHealth health) => Despawn();

        GameManager manager = GameManager.Instance;
        manager.OnStart += StartRace;
        manager.OnEnd += StopRace;
    }

    private void OnDamageTaken(float maxHp, float currentHp)
    {
        if (currentHp > 0)
        {
            hitFeedback.PlayHitResponse();
        }
    }

    protected virtual void StartRace()
    {
        movement.StartMove();
        turret.StartAim();

        effectsController.ActiveEffects(true);
    }

    protected virtual void StopRace()
    {
        movement.StopMove();
        turret.StopAim();

        effectsController.ActiveEffects(false);
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
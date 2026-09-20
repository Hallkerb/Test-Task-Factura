using UnityEngine;

public class Stickman : BaseEnemy, IAttackable
{
    [SerializeField] private StickmanMovement movement;
    [SerializeField] private StickmanAnimatorController animatorController;
    [SerializeField] private HitFeedback hitFeedback;

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

    private void OnDamageTaken(float maxHp, float currentHp)
    {
        if (currentHp > 0)
        {
            hitFeedback.PlayHitResponse();
        }
    }

    public void StartAttack(Transform car)
    {
        movement.StartMove(car);
        animatorController.StartMoveAnimation();
    }

    protected override void ResetState()
    {
        base.ResetState();

        movement.ResetState();
        animatorController.ResetState();
    }
}

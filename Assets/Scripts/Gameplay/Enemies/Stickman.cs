using UnityEngine;

public class Stickman : BaseEnemy, IAttackable
{
    private GameManager gameManager;

    [SerializeField] private StickmanMovement movement;
    [SerializeField] private StickmanAnimatorController animatorController;
    [SerializeField] private HitFeedback hitFeedback;

    protected override void Awake()
    {
        base.Awake();

        gameManager = GameManager.Instance;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (health != null)
        {
            health.OnDamageTaken += OnDamageTaken;
        }

        gameManager.OnEnd += StopAttack;
    }

    private void OnDisable()
    {
        if (health != null)
        {
            health.OnDamageTaken -= OnDamageTaken;
        }

        gameManager.OnEnd -= StopAttack;
    }

    private void OnDamageTaken(float maxHp, float currentHp)
    {
        if (currentHp > 0)
        {
            hitFeedback.PlayHitResponse();
        }
    }

    public void StopAttack()
    {
        movement.ResetState();
        animatorController.ResetState();
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

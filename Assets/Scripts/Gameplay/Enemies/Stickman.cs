using UnityEngine;

public class Stickman : BaseEnemy, IAttackable
{
    [SerializeField] private StickmanMovement movement;
    [SerializeField] private StickmanAnimatorController animatorController;

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

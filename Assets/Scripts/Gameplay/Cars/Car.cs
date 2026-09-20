using UnityEngine;

public class Car : BaseCar
{
    [SerializeField] private CarAnimatorController animatorController;

    protected override void StartRace()
    {
        base.StartRace();

        animatorController.StartMove();
    }

    protected override void StopRace()
    {
        base.StopRace();

        animatorController.StopMove();
    }
}

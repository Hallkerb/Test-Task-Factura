using UnityEngine;

public class Car : BaseCar
{
    [SerializeField] private CarAnimation carAnimation;

    protected override void StartRace()
    {
        base.StartRace();

        carAnimation.StartMove();
    }

    protected override void StopRace()
    {
        base.StopRace();

        carAnimation.StopMove();
    }
}

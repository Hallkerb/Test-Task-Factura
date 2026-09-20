using UnityEngine;

public class CarAnimatorController : MonoBehaviour
{
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float rotationSpeed = 360f;

    private bool isMoving;

    private void Update()
    {
        if (isMoving)
        {
            foreach (var wheel in wheels)
            {
                wheel.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }

    public void StartMove()
    {
        isMoving = true;
    }

    public void StopMove()
    {
        isMoving = false;
    }
}

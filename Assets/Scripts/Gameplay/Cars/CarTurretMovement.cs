using UnityEngine;

public class CarTurretMovement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 500f;

    public void RotateToPoint(Vector3 targetPoint)
    {
        Vector3 direction = targetPoint - transform.position;
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                targetRotation, 
                rotationSpeed * Time.deltaTime
            );
        }
    }

    public void ResetState()
    {
        transform.rotation = Quaternion.identity;
    }
}

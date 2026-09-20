using UnityEngine;

public class StickmanMovement : MonoBehaviour
{
    private Transform car;

    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float rotationSpeed = 200f;

    void Update()
    {
        if (car == null) return;

        Move();
    }

    public void StartMove(Transform car)
    {
        this.car = car;
    }

    private void Move()
    {
        Vector3 direction = car.position - transform.position;
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

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void ResetState()
    {
        car = null;
    }
}

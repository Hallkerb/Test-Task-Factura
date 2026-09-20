using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    private bool canMove;

    public float Speed => speed;

    void Update()
    {
        if (canMove == false) return;

        Move();
    }

    public void StartMove()
    {
        canMove = true;
    }

    public void StopMove()
    {
        canMove = false;
    }

    private void Move()
    {
        Vector3 position = transform.position;
        position.z += Time.deltaTime * speed;

        transform.position = position;
    }

    public void ResetState(Vector3 resetPosition)
    {
        transform.position = resetPosition;
    }
}

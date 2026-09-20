using System;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1;

    private bool canMove;

    public float Speed => speed;

    public event Action OnMoveStart;
    public event Action OnMoveEnd;

    void Update()
    {
        if (canMove == false) return;

        Move();
    }

    public void StartMove()
    {
        canMove = true;

        OnMoveStart?.Invoke();
    }

    public void StopMove()
    {
        canMove = false;

        OnMoveEnd?.Invoke();
    }

    private void Move()
    {
        Vector3 position = transform.position;
        position.z += Time.deltaTime * speed;

        transform.position = position;
    }

    public void ResetState()
    {
        canMove = false;
    }
}

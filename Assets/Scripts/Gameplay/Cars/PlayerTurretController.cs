using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurretController : MonoBehaviour
{
    [SerializeField] private CarTurretMovement movement;
    private Camera mainCamera;

    private bool canAim;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (canAim == false) return;

        if (TryGetPointerWorldPosition(out Vector3 targetPoint))
        {
            movement.RotateToPoint(targetPoint);
        }
    }

    public void StartAim()
    { 
        canAim = true;
    }

    public void StopAim()
    {
        canAim = false;
    }

    private bool TryGetPointerWorldPosition(out Vector3 position)
    {
        Vector3 inputPosition = GetInputPosition();

        if (inputPosition == Vector3.negativeInfinity)
        {
            position = Vector3.zero;
            return false;
        }

        Ray ray = mainCamera.ScreenPointToRay(inputPosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float entryDistance))
        {
            position = ray.GetPoint(entryDistance);
            return true;
        }

        position = Vector3.zero;
        return false;
    }

    private Vector3 GetInputPosition()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            return new Vector3(touchPos.x, touchPos.y, 0f);
        }
        else if (Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            return new Vector3(mousePos.x, mousePos.y, 0f);
        }

        return Vector3.negativeInfinity;
    }

    public void ResetState()
    {
        movement.ResetState();
    }
}

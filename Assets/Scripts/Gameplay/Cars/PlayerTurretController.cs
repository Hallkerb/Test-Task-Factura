using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTurretController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LineRenderer lineRenderer;
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

        lineRenderer.enabled = true;
    }

    public void StopAim()
    {
        canAim = false;

        lineRenderer.enabled = false;
    }

    private bool TryGetPointerWorldPosition(out Vector3 position)
    {
        if (TryGetInputPosition(out Vector2 pos) == false)
        {
            position = Vector3.zero;
            return false;
        }

        Ray ray = mainCamera.ScreenPointToRay(pos);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        if (groundPlane.Raycast(ray, out float entryDistance))
        {
            position = ray.GetPoint(entryDistance);
            return true;
        }

        position = Vector3.zero;
        return false;
    }

    private bool TryGetInputPosition(out Vector2 pos)
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
            pos = new Vector2(touchPos.x, touchPos.y);

            return true;
        }
        else if (Mouse.current != null)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            pos = new Vector2(mousePos.x, mousePos.y);

            return true;
        }

        pos = Vector2.zero;

        return false;
    }

    public void ResetState()
    {
        movement.ResetState();
    }
}

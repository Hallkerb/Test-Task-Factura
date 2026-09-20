using UnityEngine;

public class CarTurret : MonoBehaviour
{
    [SerializeField] private PlayerTurretController playerController;
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private SpawnableType bullet;

    [SerializeField] private float reloadTime = 1;
    private float remainingTime;

    private bool canAim;

    void Update()
    {
        if (canAim == false) return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0) Shoot();
    }
    
    public void StartAim()
    {
        canAim = true;

        playerController.StartAim();
    }

    public void StopAim()
    {
        canAim = false;

        playerController.StopAim();
    }

    private void Shoot()
    {
        Vector3 currentEuler = transform.rotation.eulerAngles;
        Quaternion yawRotation = Quaternion.Euler(90, currentEuler.y, 0);

        SpawnManager.Instance.Spawn(bullet, spawnPosition.position, yawRotation);

        Reload();
    }

    private void Reload()
    {
        remainingTime = reloadTime;
    }

    public void ResetState()
    {
        playerController.ResetState();
        Reload();
    }
}

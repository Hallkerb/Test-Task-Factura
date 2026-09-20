using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILevelProgress : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Image progressImage;

    [SerializeField] private Transform finish;
    private Transform car;

    [SerializeField] private Vector3 startPoint = Vector3.zero;

    private Coroutine progressCoroutine;

    void Awake()
    {
        levelManager.OnCarSpawned += InitializeCar;
    }

    private void OnEnable()
    {
        gameManager.OnStart += StartTracking;
        gameManager.OnEnd += StopTracking;
    }

    private void OnDisable()
    {
        gameManager.OnStart -= StartTracking;
        gameManager.OnEnd -= StopTracking;
    }

    private void StartTracking()
    {
        StopTracking();
        progressCoroutine = StartCoroutine(TrackProgressRoutine());
    }

    private void StopTracking()
    {
        if (progressCoroutine != null)
        {
            StopCoroutine(progressCoroutine);
            progressCoroutine = null;
        }
    }

    private void ResetState()
    {
        progressImage.fillAmount = 0;
    }

    private IEnumerator TrackProgressRoutine()
    {
        if (finish.position.z == startPoint.z) yield break;

        while(car.position.z < finish.position.z)
        {
            progressImage.fillAmount = (car.position.z - startPoint.z) / (finish.position.z - startPoint.z);

            yield return null;
        }

        progressImage.fillAmount = 1;

        progressCoroutine = null;
    }

    private void InitializeCar(BaseCar car)
    {
        this.car = car.transform;

        ResetState();
    }
}

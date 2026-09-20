using System;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public event Action OnFinished;

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out BaseCar car))
            OnFinished?.Invoke();
    }
}

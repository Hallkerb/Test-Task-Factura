using System;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private Transform car;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IAttackable enemy))
        {
            enemy.StartAttack(car);
        }
    }
}

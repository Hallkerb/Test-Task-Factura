using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] private Team team;

    [SerializeField] private Vector3 uiOffset;

    [SerializeField] private float maxHP = 1;
    private float currentHP;

    public float HP => currentHP;
    public float MaxHP => maxHP;

    public Team Team => team;

    public Vector3 UIOffset => uiOffset;

    public event Action<float, float> OnDamageTaken;
    public event Action<IHealth> OnDied;
    public event Action<IHealth> OnReseted;

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        OnDamageTaken?.Invoke(maxHP, currentHP);
        
        if (currentHP < 1) OnDied?.Invoke(this);
    }

    public void ResetState()
    {
        currentHP = maxHP;

        OnReseted?.Invoke(this);
    }
}

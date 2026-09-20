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

    private bool isDead;

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;

        OnDamageTaken?.Invoke(maxHP, currentHP);
        
        if (currentHP <= 0)
        {
            isDead = true;

            OnDied?.Invoke(this);
        }
    }

    public void ResetState()
    {
        isDead = false;

        currentHP = maxHP;
    }
}

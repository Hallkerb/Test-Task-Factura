using System;
using UnityEngine;

public interface IHealth : ITeamMember
{
    float MaxHP { get; }
    float HP { get; }

    Vector3 UIOffset { get; }

    event Action<float, float> OnDamageTaken;
    event Action<IHealth> OnDied;

    void TakeDamage(float damage);

    void ResetState();
}

using System;
using UnityEngine;

public interface ISpawnable
{
    SpawnableType SpawnableType { get; }

    GameObject GameObject { get; }

    event Action<SpawnableType, ISpawnable> OnDespawn;

    void Despawn();
}

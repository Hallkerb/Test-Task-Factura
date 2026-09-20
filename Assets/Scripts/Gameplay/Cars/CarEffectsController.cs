using UnityEngine;

public class CarEffectsController : MonoBehaviour
{
    [SerializeField] private TrailRenderer[] trail;

    [SerializeField] private ParticleSystem particle; 

    private void OnEnable()
    {
        if (trail != null)
        {
            for (int i = 0; i < trail.Length; i++)
            {
                if (trail[i] != null)
                    trail[i].Clear();
            }
        }

        if (particle != null)
            particle.Clear();
    }

    public void ActiveEffects(bool value)
    {
        if (trail != null)
        {
            for (int i = 0; i < trail.Length; i++)
            {
                if (trail[i] != null)
                    trail[i].emitting = value;
            }
        }

        if (particle != null)
        {
            if (value)
                particle.Play();
            else
                particle.Stop();
        }
    }
}

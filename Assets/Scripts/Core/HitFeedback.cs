using System.Collections;
using UnityEngine;

public class HitFeedback : MonoBehaviour
{
    [Header("Hit Flash Settings")]
    [SerializeField] private Renderer characterRenderer;
    [SerializeField] private float flashDuration = 0.08f;

    private Material material;
    private Coroutine flashCoroutine;

    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        if (characterRenderer != null)
        {
            material = characterRenderer.material;
        }
    }

    public void PlayHitResponse()
    {
        if (material == null) return;

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(HitFlashRoutine());
    }

    private IEnumerator HitFlashRoutine()
    {
        material.EnableKeyword("_EMISSION");
        material.SetColor(EmissionColorID, Color.white * 2f);

        yield return new WaitForSeconds(flashDuration);

        material.SetColor(EmissionColorID, Color.black);
        material.DisableKeyword("_EMISSION");

        flashCoroutine = null;
    }
}

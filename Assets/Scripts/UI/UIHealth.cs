using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    private Camera mainCamera;

    private IHealth health;
    private Transform healthTransform;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image healthImage;
    [SerializeField] private Image damageImage;

    [SerializeField] private float animationDamageDuration = 0.5f;

    private Vector3 offset;

    private bool isActive = true;

    private Coroutine healthCoroutine;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnDisable()
    {
        Untrack(health);
    }

    void LateUpdate()
    {
        if (health == null) return;

        Vector3 screenPoint = mainCamera.WorldToScreenPoint(healthTransform.position + offset);

        transform.position = screenPoint;

        if (health.Team != Team.Player && health.HP == health.MaxHP)
        {
            if (isActive)
                SetVisible(false);

            return;
        }

        if (screenPoint.z > 0)
        {
            if (isActive == false) 
                SetVisible(true);
        }
        else
        {
            if (isActive)
                SetVisible(false);
        }
    }

    private void DamageTaken(float maxHP, float currentHP)
    {
        healthImage.fillAmount = currentHP / maxHP;

        StopHealthCoroutine();

        healthCoroutine = StartCoroutine(TrackDamageRoutine());
    }

    private void StopHealthCoroutine()
    {
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
            healthCoroutine = null;
        }
    }

    private IEnumerator TrackDamageRoutine()
    {
        float startDamageFill = damageImage.fillAmount;

        float timeLeft = 0;

        while(timeLeft < animationDamageDuration)
        {
            timeLeft += Time.deltaTime;

            damageImage.fillAmount = Mathf.Lerp(startDamageFill, healthImage.fillAmount, timeLeft / animationDamageDuration);

            yield return null;
        }

        damageImage.fillAmount = healthImage.fillAmount;

        healthCoroutine = null;
    }

    public void SetVisible(bool value)
    {
        isActive = value;

        backgroundImage.enabled = value;
        healthImage.enabled = value;
        damageImage.enabled = value;
    }

    private void Follow(IHealth health)
    {
        if (health == null) return;

        healthImage.fillAmount = health.HP / health.MaxHP;
        damageImage.fillAmount = health.HP / health.MaxHP;

        health.OnDamageTaken += DamageTaken;
    }

    private void UnFollow(IHealth health)
    {
        if (health == null) return;

        health.OnDamageTaken -= DamageTaken;

        StopHealthCoroutine();
    }

    private void Untrack(IHealth health)
    {
        UnFollow(health);

        this.health = null;
        healthTransform = null;
    }

    public void Initialize(IHealth health, Transform healthTransform)
    {
        Untrack(this.health);

        this.health = health;
        this.healthTransform = healthTransform;

        offset = health.UIOffset;

        Follow(health);
    }
}

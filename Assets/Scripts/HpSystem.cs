using System;
using System.Collections;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    public const int maxHitPoints = 100;

    [SerializeField] private int hitPoints = maxHitPoints;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private static Color damagedColor = new Color(1, 0.3137255f, 0);

    private Color prevColor;
    private Coroutine ColorFeedbackCoroutine;
    public Action<int> OnValueChanged;
    public Action OnDeath;
    public bool isAlive => hitPoints > 0;

    private void Start()
    {
        prevColor = spriteRenderer.color;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            hitPoints -= 20;
            OnValueChanged?.Invoke(hitPoints);
            if (hitPoints <= 0)
            {
                gameObject.SetActive(false);
                OnDeath.Invoke();
            }
            else
            {
                if (ColorFeedbackCoroutine != null)
                    StopCoroutine(ColorFeedbackCoroutine);

                ColorFeedbackCoroutine = StartCoroutine(ColorFeedbackRoutine());
            }
        }
    }

    private IEnumerator ColorFeedbackRoutine()
    {
        spriteRenderer.color = damagedColor;
        yield return new WaitForSecondsRealtime(0.15f);
        spriteRenderer.color = prevColor;
        ColorFeedbackCoroutine = null;
    }
}

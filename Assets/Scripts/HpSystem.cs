using System;
using System.Collections;
using UnityEngine;

public class HpSystem : MonoBehaviour
{
    [SerializeField] private int hitPoints = 100;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private static Color damagedColor = new Color(1, 0.3137255f, 0);
    public Action<int> OnValueChanged { get; set; }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            hitPoints -= 20;
            OnValueChanged?.Invoke(hitPoints);
            if (hitPoints <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(ColorFeedbackRoutine());
            }
            
        }
    }

    private IEnumerator ColorFeedbackRoutine()
    {
        var prevColor = spriteRenderer.color;
        spriteRenderer.color = damagedColor;
        yield return new WaitForSecondsRealtime(0.15f);
        spriteRenderer.color = prevColor;
    }
}

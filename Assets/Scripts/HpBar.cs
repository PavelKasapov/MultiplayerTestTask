using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    public void SetHpSystem(HpSystem hpSystem)
    {
        gameObject.SetActive(true);
        slider.maxValue = HpSystem.maxHitPoints;
        hpSystem.OnValueChanged = SetScrollbarValue;
    }

    private void SetScrollbarValue(int value)
    {
        slider.value = value;
        if (text != null)
            text.text = value.ToString();
    }
}

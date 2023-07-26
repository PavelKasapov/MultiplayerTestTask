using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] HpSystem hpSystem;

    private void Awake()
    {
        hpSystem.OnValueChanged += SetScrollbarValue;
    }

    private void SetScrollbarValue(int value)
    {
        slider.value = value;
        text.text = value.ToString();
    }
}

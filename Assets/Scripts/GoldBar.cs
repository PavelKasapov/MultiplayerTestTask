using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CoinCollectSystem coinCollectSystem;

    private void Awake()
    {
        coinCollectSystem.OnValueChanged += SetScrollbarValue;
    }

    private void SetScrollbarValue(int value)
    {
        text.text = value.ToString();
    }
}

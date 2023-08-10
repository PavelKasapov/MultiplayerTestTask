using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void SetCoinSystem(CoinCollectionSystem coinCollectSystem)
    {
        coinCollectSystem.OnValueChanged = SetScrollbarValue;
    }

    private void SetScrollbarValue(int value)
    {
        text.text = value.ToString();
    }
}

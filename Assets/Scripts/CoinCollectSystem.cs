using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectSystem : MonoBehaviour
{
    public int coinsCollected = 0;
    public Action<int> OnValueChanged { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            collision.gameObject.SetActive(false);
            coinsCollected++;
            OnValueChanged?.Invoke(coinsCollected);
        }
    }
}

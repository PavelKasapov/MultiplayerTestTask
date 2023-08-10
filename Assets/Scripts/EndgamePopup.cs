using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndgamePopup : MonoBehaviour
{
    const string WinHeader = "Win!";
    const string LoseHeader = "Defeat!";
    const string DrawHeader = "Draw!";
    [SerializeField] TextMeshProUGUI WinLoseHeader;
    [SerializeField] TextMeshProUGUI WinnerText;
    [SerializeField] TextMeshProUGUI CoinsCollectedText;
    [SerializeField] Button returnButton;

    private void Start()
    {
        returnButton.onClick.AddListener(ClosePopup);
    }

    private void OnDestroy()
    {
        returnButton.onClick.RemoveListener(ClosePopup);
    }

    public void Show(Player winner)
    {
        if (winner == null)
        {
            WinLoseHeader.text = DrawHeader;
            WinnerText.text = "";
            CoinsCollectedText.text = "";
        }
        else
        {
            WinLoseHeader.text = winner.Account.isCurrentPlayer ? WinHeader : LoseHeader;
            WinnerText.text = $"Winner is {winner.Account.accountName}";
            CoinsCollectedText.text = $"{winner.Account.accountName} collected {winner.coinCollectionSystem.coinsCollected} coins";
        }
        gameObject.SetActive(true);
    }

    private void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}

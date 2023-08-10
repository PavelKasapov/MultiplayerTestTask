using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManagementSystem : MonoBehaviour
{
    [SerializeField] private BulletManager bulletManager;
    [SerializeField] private PlayerInput inputAdapter;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private HpBar mainHpBar;
    [SerializeField] private GoldCounter goldCounter;
    [SerializeField] private EndgamePopup endgamePopup;

    private List<Player> allPlayers = new List<Player>();
    private Player currentPlayer;

    private void Start()
    {
        //Псевдоаккаунты - Временное решение
        var accounts = new List<Account>();
        var nicknameSample = "Nickname";
        for (int i = 0; i < 4; i++)
        {
            var newAccount = new Account()
            {
                accountName = $"{nicknameSample}{i + 1}",
                id = $"{i}",
                isCurrentPlayer = i == 0,
            };
            accounts.Add(newAccount);
        }
        SpawnPlayers(accounts);
        // ----- 
    }
    public void SpawnPlayers(List<Account> playerAccounts)
    {
        return;
        for (int i = 0; i < playerAccounts.Count; i++)
        {
            Account account = playerAccounts[i];
            var newPlayer = Instantiate(playerPrefab, spawnPoints[i].transform.position, Quaternion.identity, transform).GetComponent<Player>();
            newPlayer.Account = account;
            newPlayer.playerMovementSystem.bulletManager = bulletManager;
            newPlayer.selfHpBar.SetHpSystem(newPlayer.hpSystem);
            newPlayer.hpSystem.OnDeath = OnPlayerDeath;
            if (account.isCurrentPlayer)
            {
                currentPlayer = newPlayer;
                inputAdapter.SetPlayer(newPlayer);
                newPlayer.selfHpBar.gameObject.SetActive(false);
                mainHpBar.SetHpSystem(newPlayer.hpSystem);
                goldCounter.SetCoinSystem(newPlayer.coinCollectionSystem);
            }
            allPlayers.Add(newPlayer);
        }
    }

    private void OnPlayerDeath()
    {
        var alivePlayers = allPlayers.Where(player => player.hpSystem.isAlive).ToList();
        Debug.Log(alivePlayers.Count);
        if (alivePlayers.Count <= 1)
        {
            var winner = alivePlayers.FirstOrDefault();
            endgamePopup.Show(winner);
            //Show endgame Popup;
        }
    }
}

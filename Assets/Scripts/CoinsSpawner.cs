using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinsSpawner : NetworkBehaviour
{
    [SerializeField] CoinPattern coinPattern;
    [SerializeField] GameObject coinPrefab;

    public override void OnNetworkSpawn() => ServerSpawnCoins();
    
    private void ServerSpawnCoins()
    {
        if (IsServer)
        {
            foreach (var position in coinPattern.CoinPositions)
            {
                var coinNetworkObject = Instantiate(coinPrefab, position, Quaternion.identity, transform).GetComponent<NetworkObject>();
                coinNetworkObject.Spawn(true);
                coinNetworkObject.TrySetParent(gameObject);
            }
        }
    }
}

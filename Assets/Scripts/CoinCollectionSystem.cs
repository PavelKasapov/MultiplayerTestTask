using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinCollectionSystem : NetworkBehaviour
{
    public NetworkVariable<int> coinsCollected = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Action<int> OnValueChanged { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            if (IsOwner)
            {
                CollectCoinServerRpc(collision.GetComponent<NetworkObject>());
            }
        }
    }

    [ServerRpc]
    private void CollectCoinServerRpc(NetworkObjectReference target)
    {
        CollectCoinClientRpc(target);
        coinsCollected.Value++;
        OnValueChanged?.Invoke(coinsCollected.Value);
    }

    [ClientRpc]
    private void CollectCoinClientRpc(NetworkObjectReference target)
    {
        if (target.TryGet(out NetworkObject targetObject))
        {
            targetObject.gameObject.SetActive(false);
        }
    }
}

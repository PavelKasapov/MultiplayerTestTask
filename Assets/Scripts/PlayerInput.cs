using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerInput : NetworkBehaviour
{
    private PlayerControls _playerActionMap;
    private Player player;

    public override void OnNetworkSpawn()
    {
        SetPlayer(GetComponent<Player>());
        base.OnNetworkSpawn();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
        Debug.Log($"!IsOwner {IsOwner}");
        if (!IsOwner) return;

        _playerActionMap?.Dispose();
        _playerActionMap = new PlayerControls();
        _playerActionMap.Enable();

        /*_playerActionMap.Player.MovementDirection.performed += ctx => player.playerMovementSystem.Move(ctx.ReadValue<Vector2>());
        _playerActionMap.Player.MovementDirection.canceled += ctx => player.playerMovementSystem.Move(ctx.ReadValue<Vector2>());*/

        _playerActionMap.Player.MovementDirection.performed += ctx =>
        {
            player.playerMovementSystem.Move(ctx.ReadValue<Vector2>());
            MoveServerRpc(ctx.ReadValue<Vector2>(), NetworkObjectId);
        };
        _playerActionMap.Player.MovementDirection.canceled += ctx => {
            player.playerMovementSystem.Move(ctx.ReadValue<Vector2>());
            MoveServerRpc(ctx.ReadValue<Vector2>(), NetworkObjectId);
        };
        _playerActionMap.Player.Shoot.performed += ctx => player.playerMovementSystem.ShootServerRpc();
    }

    [ClientRpc]
    public void MoveClientRpc(Vector2 direction, ulong originNetworkObjectId)
    {
        if (IsOwner && originNetworkObjectId == NetworkObjectId) 
            return;

        player.playerMovementSystem.Move(direction);
        Debug.Log($"Move object from position {transform.position} in direction {direction}");
    }

    [ServerRpc]
    public void MoveServerRpc(Vector2 direction, ulong originNetworkObjectId)
    {
        MoveClientRpc(direction, originNetworkObjectId);
    }

    public override void OnDestroy()
    {
        _playerActionMap.Disable();
        base.OnDestroy();
    }
}

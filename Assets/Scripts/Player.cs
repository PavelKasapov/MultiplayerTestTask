using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovementSystem playerMovementSystem;
    public CoinCollectionSystem coinCollectionSystem;
    public HpSystem hpSystem;
    public HpBar selfHpBar;

    public Account Account;
}

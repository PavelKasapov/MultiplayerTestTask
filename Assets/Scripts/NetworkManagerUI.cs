using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button HostBtn;
    [SerializeField] Button ClientBtn;

    private void Awake()
    {
        HostBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        ClientBtn.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
    }
}

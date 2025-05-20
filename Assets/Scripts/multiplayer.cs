using Unity.Netcode;
using UnityEngine;
using TMPro;

public class multiplayer : MonoBehaviour
{
    public void HostGame()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void JoinGame()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void Update()
    {
        int connectedPlayers = NetworkManager.Singleton.ConnectedClients.Count;
        if (GameObject.Find("ConnectedCounter") != null)
        {
            GameObject.Find("ConnectedCounter").GetComponent<TextMeshProUGUI>().text = connectedPlayers.ToString();
        }
    }
}

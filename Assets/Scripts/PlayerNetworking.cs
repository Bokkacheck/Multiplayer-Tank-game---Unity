using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerNetworking : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;
    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        string playerName = "Player" + GetComponent<NetworkIdentity>().netId;
        transform.name = playerName;
        PlayerStatus player = GetComponent<PlayerStatus>();
        player.PlayerName = playerName;
        PlayersManager.AddPlayer(playerName, player);
    }
    public void OnDisable()
    {
        PlayersManager.DeletePlayer(transform.name);
    }
}

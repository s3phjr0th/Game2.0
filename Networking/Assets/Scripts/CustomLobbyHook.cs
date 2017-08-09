using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
public class CustomLobbyHook : LobbyHook {
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobbyPlayerComponent = lobbyPlayer.GetComponent<LobbyPlayer>();
        PlayerNetwork playerNetwork = gamePlayer.GetComponent<PlayerNetwork>();
        playerNetwork.playerName = lobbyPlayerComponent.playerName;
        playerNetwork.playerColor = lobbyPlayerComponent.playerColor;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameplayManager : NetworkBehaviour
{
    public static GameplayManager Instance { get; private set; }

    public List<PlanetInfo> planetSeeds;
    public GameObject planetPrefab;
    public Transform planetsContainer;

    [HideInInspector]
    public List<PlanetController> planets;

    public List<PlayerNetwork> players = new List<PlayerNetwork>();

    private ServerManager serverManager;

    private void Awake()
    {
        Instance = this;

        foreach (PlanetInfo info in planetSeeds)
        {
            PlanetController newPlanet = TKUtils.Instantiate<PlanetController>(planetPrefab, planetsContainer);
            newPlanet.Init(info);
            planets.Add(newPlanet);
        }
    }

    private void Start()
    {
        if (isServer)
        {
            serverManager = gameObject.AddComponent<ServerManager>();
        }
    }

    public void AddPlayer(PlayerNetwork player)
    {
        players.Add(player);
    }

    public void OnPlayerTurnEnded()
    {
        serverManager.OnPlayerTurnEnded();
    }

    internal void OnBulletHitPlayer(PlayerNetwork player)
    {
        players.Remove(player);

        if (isServer)
        {
            player.RpcDie();
        }
    }
}
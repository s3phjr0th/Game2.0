using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkDiscovery : NetworkDiscovery {
    private static Dictionary<string,string> serverList = new Dictionary<string,string>();

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        serverList[fromAddress] = data;
    }

    public Dictionary<string, string> GetActiveServers()
    {
        Dictionary<string,string> result = serverList;
        serverList = new Dictionary<string, string>();

        return result;
    }
}

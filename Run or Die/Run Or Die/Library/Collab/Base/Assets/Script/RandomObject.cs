using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RandomObject : MonoBehaviour {
	public GameObject map;		//Ve sau de List<>
    public GameObject wall;
    public GameObject poison;

    public float startX;
	public List<Transform> players;
	public float distanceMap;

    private int currentPlatformIndex = -1;
	private float spawnHalfWidth;

	private List<GameObject> platformGroundPool = new List<GameObject>();
    private List<GameObject> platformTrapPool = new List<GameObject>();
    private void Start()
	{
		
		CreateNewPlatformIfNeeded();

	}
    private void Update()
    {
        CreateNewPlatformIfNeeded();
        //144
        foreach (GameObject platform in platformGroundPool)
        {
            if (platform.transform.position.x < Camera.main.transform.position.x - 2 * distanceMap)
            {
                platform.SetActive(false);
            }
        }
    }

    private GameObject GetNewPlatformGround()
	{
		foreach(GameObject platform in platformGroundPool)
		{
			if (!platform.activeInHierarchy)
			{
				return platform;
			}
		}
        GameObject newPlatform = Instantiate(
			map,
			Vector3.zero,
			Quaternion.identity
		);
		platformGroundPool.Add(newPlatform);

		return newPlatform;
	}

    private GameObject GetNewPlatformTrap()
    {
        foreach (GameObject platform in platformTrapPool)
        {
            if (!platform.activeInHierarchy)
            {
                return platform;
            }
        }
        GameObject trap  = (Random.Range(0, 2) > 0) ? wall : poison;
        GameObject newPlatform = Instantiate(
            trap,
            Vector3.zero,
            Quaternion.identity
        );
        platformTrapPool.Add(newPlatform);

        return newPlatform;
    }

    private void CreateNewPlatformIfNeeded()
	{
		while (players.Max((player) => player.position.x) > startX + currentPlatformIndex * distanceMap - distanceMap)
		{
			GameObject newPlatformGround = GetNewPlatformGround();
            newPlatformGround.transform.position = new Vector3(currentPlatformIndex * distanceMap, -60 , 1); // y la day camera (-90).
            newPlatformGround.SetActive(true);
            RandomGround(newPlatformGround);
            newPlatformGround.transform.localScale = new Vector3(1, 1, 1);
            currentPlatformIndex++;


            GameObject newPlatformTrap = GetNewPlatformTrap();
            newPlatformTrap.transform.position = new Vector3(currentPlatformIndex * distanceMap, Random.Range(0,60), 1); // y la day camera (-90).
            newPlatformTrap.SetActive(true);
            RandomTrap(newPlatformTrap);
            newPlatformGround.transform.localScale = new Vector3(1, 1, 1);

        }
	}


    private void RandomTrap(GameObject newTrap)
    {
        newTrap.transform.localScale = new Vector3(1, Random.Range(0.5f, 1.5f), 1);
    }
	private void RandomGround(GameObject newMap)
    {
        Transform[] grounds = newMap.GetComponentsInChildren<Transform>();
        foreach (Transform ground in grounds)
        {
            ground.transform.localScale = new Vector3(1, Random.Range(0.5f, 4f), 1);
        }
    }
}

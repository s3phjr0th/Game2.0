using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory : MonoBehaviour {
    public GameObject obstaclePrefab;
    public float startY;
    public float platformSpacing;
    public float platformGapHalfWidth;

    private int currentPlatformIndex = 0;
    private float spawnHalfWidth;

    private List<GameObject> platformPool = new List<GameObject>();

    private void Start()
    {
        spawnHalfWidth = Camera.main.aspect * Camera.main.orthographicSize - platformGapHalfWidth;

        CreateNewPlatformIfNeeded();
    }

    private GameObject GetNewPlatform()
    {
        foreach(GameObject platform in platformPool)
        {
            if (!platform.activeInHierarchy)
            {
                return platform;
            }
        }

        GameObject newPlatform = Instantiate(
            obstaclePrefab,
            Vector3.zero,
            Quaternion.identity
        );

        platformPool.Add(newPlatform);

        return newPlatform;
    }

    private void CreateNewPlatformIfNeeded()
    {
        while (currentPlatformIndex * platformSpacing + startY < Camera.main.transform.position.y + Camera.main.orthographicSize)
        {
            GameObject newPlatform = GetNewPlatform();
            newPlatform.transform.position = new Vector3(Random.Range(-spawnHalfWidth, spawnHalfWidth), currentPlatformIndex * platformSpacing + startY, 0);
            newPlatform.SetActive(true);

            currentPlatformIndex++;
        }
    }

    private void Update()
    {
        CreateNewPlatformIfNeeded();

        foreach(GameObject platform in platformPool)
        {
            if(platform.transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
            {
                platform.SetActive(false);
            }
        }
    }
}

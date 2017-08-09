using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform playerTransform;

	void Update () {
        if(playerTransform.position.y > Camera.main.transform.position.y)
        {
            Camera.main.transform.position = Camera.main.transform.position.WithY(
                playerTransform.position.y
            );
        }
    }
}

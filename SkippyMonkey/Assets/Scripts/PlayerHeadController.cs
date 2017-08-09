using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadController : MonoBehaviour {

    private void OnCollisionEnter2D (Collision2D collision)
    {
        TKSceneManager.ChangeScene("GameOver");
    }
}

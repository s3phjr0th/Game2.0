using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour {

	public void Begin()
    {
        TKSceneManager.ChangeScene("Level1");
    }
}

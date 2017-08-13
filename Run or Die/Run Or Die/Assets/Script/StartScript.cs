using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour {
	public Button startButton;
	public Button settingButton;
	public Button helpButton;

	public void start (){
            SceneManager.LoadScene("CharacterScene");
	}
	public void setting (){
	}
	public void help (){
	}
}

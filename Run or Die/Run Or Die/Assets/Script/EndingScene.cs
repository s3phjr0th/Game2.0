using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndingScene : MonoBehaviour {
    public Button restartButton;
    public Button homeButton;
    private void Start()
    {
        Debug.Log("" + PlayerPrefs.GetString("WINNER"));
    }
    public void Restart()
    {
        SceneManager.LoadScene("CharacterScene");
    }
    public void Home()
    {
        SceneManager.LoadScene("StartScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseController : MonoBehaviour {

    private bool isFreeze = false;
    public Button pauseButton;
    public Button resumeButton;
    public Button homeButton;
    public Button retryButton;
    public AudioClip ButtonClick;
    public Image advImage;
    // Use this for initialization
    void Start () {
        freezeScreen();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void freezeScreen()
    {
        if (isFreeze)
        {
            AudioSource.PlayClipAtPoint(ButtonClick, Vector3.zero);
            Time.timeScale = 0;
            pauseButton.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            advImage.gameObject.SetActive(true);

            isFreeze = !isFreeze;
        }
        else
        {
            pauseButton.gameObject.SetActive(true);
            resumeButton.gameObject.SetActive(false);
            homeButton.gameObject.SetActive(false);
            retryButton.gameObject.SetActive(false);
            advImage.gameObject.SetActive(false);
            Time.timeScale = 1;
            isFreeze = !isFreeze;
        }
    }
    public void retry ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        freezeScreen();
    }
    public void home()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1;
    }
}

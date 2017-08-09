using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndingScript : MonoBehaviour {
    public Button HomeButton;
    public Button RetryButton;
    public Text endScore;
    public Text highScore;
    public AudioClip ButtonClick;

    private void Start()
    {
        Debug.Log("End Color = " + PlayerPrefs.GetInt("SCORE"));
        endScore.text = "" + PlayerPrefs.GetInt("SCORE");
        highScore.text= "" + PlayerPrefs.GetInt("HIGHSCORE");
    }

    public void HomeClicked()
    {
        if (HomeButton == true)
        { SceneManager.LoadScene("StartScene");
          AudioSource.PlayClipAtPoint(ButtonClick, Vector3.zero);
        }

    }
    public void RetryClicked()
    {
        if (RetryButton == true)
        { SceneManager.LoadScene("PlaySceneLv1");
          AudioSource.PlayClipAtPoint(ButtonClick, Vector3.zero);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour {
    public Button pauseButton;
	public GameObject player1;
	public GameObject player2;
    public PlayScript playScript;

	public Image healthBarPlayer1;
	public Image healthBarPlayer2;
	public float healthScale;

    private void Start()
    {
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }
    void Update(){

		if (player1.transform.position.x > player2.transform.position.x+178) {
			healthBarPlayer2.transform.localScale -= new Vector3(healthScale,0,0);
		}
		if (player2.transform.position.x > player1.transform.position.x + 178) {
			healthBarPlayer1.transform.localScale -= new Vector3 (healthScale, 0, 0);
		}
		if (healthBarPlayer2.transform.localScale.x <= 0) 
		{
            PlayerPrefs.SetString("WINNER", "Player1 Win!!!");
            SceneManager.LoadScene("EndScene");
			healthScale = 0f;
		}
        else if (healthBarPlayer1.transform.localScale.x <= 0)
        {
            PlayerPrefs.SetString("WINNER", "Player2 Win!!!");
            SceneManager.LoadScene("EndScene");
            healthScale = 0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateKeeper : MonoBehaviour
{
    public string levelNumber;
    public string password;
    public string nextSceneName;
    public List<GameObject> hints;

    public Text levelText;
    public InputField passwordInput;
    public Text accessDeniedText;
    public Button nextHintButton;

    private int currentHintIndex = 0;

    private void Start()
    {
        if(hints.Count > 1)
        {
            nextHintButton.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad % 2 < 1)
        {
            levelText.text = "LEVELS";
        }
        else
        {
            levelText.text = levelNumber;
        }

        if(Input.GetKey(KeyCode.Return))
        {
            CheckPassword();
        }
    }

    public void CheckPassword()
    {
        if (string.IsNullOrEmpty(passwordInput.text.Trim()))
            return;

        if (passwordInput.text.Trim() == password)
        {
            TKSceneManager.ChangeScene(nextSceneName);
        }
        else
        {
            passwordInput.text = "";
            accessDeniedText.gameObject.SetActive(true);
        }
    }

    public void OnHintButtonClicked()
    {
        currentHintIndex = (currentHintIndex + 1) % hints.Count;

        if (currentHintIndex == hints.Count - 1)
        {
            nextHintButton.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            nextHintButton.transform.localScale = Vector3.one;
        }

        for(int i = 0; i < hints.Count; i++)
        {
            hints[i].SetActive(i == currentHintIndex);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public AudioClip ButtonClick;
    public void Begin()
    {
        AudioSource.PlayClipAtPoint(ButtonClick, Vector3.zero);
        SceneManager.LoadScene("PlaySceneLv1");
    }
    public void HelpScene()
    {
        AudioSource.PlayClipAtPoint(ButtonClick, Vector3.zero);
        SceneManager.LoadScene("TutorialScene");
    }
}

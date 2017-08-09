using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{

    public AudioClip ButtonClick;
    public void Home()
    {
        AudioSource.PlayClipAtPoint(ButtonClick, Vector3.zero);
        SceneManager.LoadScene("StartScene");
    }
}

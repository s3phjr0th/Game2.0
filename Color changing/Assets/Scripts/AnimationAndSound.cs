using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndSound : MonoBehaviour {
    public AudioClip ButtonClick;
    public int TimeLoop;
    private void Start()
    {
        StartCoroutine(ButtonClickSound());
    }
    private IEnumerator ButtonClickSound()
    {
        while (true)
        {
            AudioSource.PlayClipAtPoint(ButtonClick, Vector3.zero);
            yield return new WaitForSeconds(TimeLoop);
        }
    }
}

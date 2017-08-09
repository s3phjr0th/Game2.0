using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAndSound : MonoBehaviour {
    public AudioClip UIClickSound;

    private void Start()
    {
        StartCoroutine(MoveRight(300));
        StartCoroutine(PlayClickSoundRegularly());
    }

    private IEnumerator PlayClickSoundRegularly()
    {
        while (true)
        {
            AudioSource.PlayClipAtPoint(UIClickSound, Vector3.zero);
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator MoveRight(float distance)
    {
        // Method 1
        //float distanceMoved = 0;

        //while (distanceMoved < distance)
        //{
        //    transform.localPosition += Vector3.right * Time.deltaTime * 100;
        //    distanceMoved += Time.deltaTime * 100;

        //    yield return null;
        //}


        // Method 2
        float timeToAnimate = distance / 100;
        float time = 0;
        Vector2 startingPosition = transform.localPosition;
        Vector2 targetPosition = transform.localPosition + (Vector3.right * distance);

        while (time < timeToAnimate)
        {
            // Lerp = Linear Interpolation
            transform.localPosition = Vector2.Lerp(
                startingPosition,
                targetPosition,
                time / timeToAnimate
            );

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour {
    private PlayerNetwork playerNetwork;

    private Coroutine observerCoroutine;

    void Awake () {
        playerNetwork = GetComponent<PlayerNetwork>();
	}
	
	public void OnServerPositionChanged(PlayerStatus playerStatus)
    {
        if (observerCoroutine != null)
        {
            StopCoroutine(observerCoroutine);
        }

        observerCoroutine = StartCoroutine(MoveTo(playerStatus.position));
    }

    IEnumerator MoveTo(Vector2 newPos)
    {
        Vector2 startPosition = transform.position;
        float time = 0;
        while (time < 0.1f)
        {
            transform.position = Vector2.Lerp(startPosition, newPos, time / 0.1f);

            time += Time.deltaTime;
            yield return null;
        }
        transform.position = newPos;
        observerCoroutine = null;
    }

    public void Fire(Vector2 position, Vector2 direction)
    {
        BulletController.CreateBullet(
            playerNetwork.bulletPrefab,
            position,
            direction
        );
    }
}

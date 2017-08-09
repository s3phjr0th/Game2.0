using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    public float initialSpeed;
    public float noCollisionDistance;
    public LayerMask collisionLayer;
    public float gravityWeight;

    private Vector2 velocity;
    private float timeSinceAlive;
    private float invincibleTime;
    private PlayerNetwork parent;
    private bool isActive = true;

    [Component]
    SpriteRenderer sr;

    public static void CreateBullet(GameObject prefab, Vector2 position, Vector2 direction, PlayerNetwork parent = null)
    {
        BulletController newBullet = TKUtils.Instantiate<BulletController>(prefab);
        newBullet.Init(direction, position, parent);

        CameraFollow.Instance.target = newBullet.transform;
    }

    private void Awake()
    {
        invincibleTime = noCollisionDistance / initialSpeed;
        velocity = Vector2.zero;

        this.LoadComponents();
    }

    public void Init(Vector2 direction, Vector2 position, PlayerNetwork parent)
    {
        this.parent = parent;
        transform.position = position;
        velocity = direction.ScaleTo(initialSpeed);
        transform.localScale = (Vector2.one * 0.02f).WithZ(1);
        timeSinceAlive = 0;
    }

    private void FixedUpdate()
    {
        if (!isActive) return;

        foreach (PlanetController planet in GameplayManager.Instance.planets)
        {
            velocity += (Vector2)(planet.transform.position - transform.position).ScaleTo(Time.fixedDeltaTime * gravityWeight * planet.Size * planet.Size / (planet.transform.position - transform.position).sqrMagnitude);
        }

        if(timeSinceAlive > invincibleTime)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, velocity, velocity.magnitude * Time.fixedDeltaTime, collisionLayer);
            if (hit)
            {
                if (hit.collider.tag == "Player")
                {
                    GameplayManager.Instance.OnBulletHitPlayer(hit.collider.GetComponent<PlayerNetwork>());
                }
                StartCoroutine(Explode());
                return;
            }
        }
        
        transform.position += (Vector3)velocity * Time.fixedDeltaTime;

        timeSinceAlive += Time.fixedDeltaTime;
        if (timeSinceAlive > lifeTime) StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        isActive = false;

        float time = 0;
        while (time < 0.2f)
        {
            //sr.color = Color.Lerp(startColor, endColor, time / 0.3f);
            transform.localScale = Vector3.one * Mathf.Lerp(0.1f, 0.5f, Mathfx.Sinerp(0, 1, time / 0.2f));

            time += Time.deltaTime;
            yield return null;
        }
        sr.enabled = false;

        yield return new WaitForSeconds(1);
        if(parent !=null)
        {
            GameplayManager.Instance.OnPlayerTurnEnded();
        }

        DestroyObject(gameObject);
    }
}

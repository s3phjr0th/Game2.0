using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    private float size;
    public float Size
    {
        get
        {
            return size;
        }
        set
        {
            sr.transform.localScale = Vector3.one * 2 * value / sr.sprite.textureRect.size.x;
            shipCollider.radius = value + shipRadius;
            size = value;
        }
    }

    public float shipRadius;
    public float bulletRadius;
    public CircleCollider2D shipCollider;
    public SpriteRenderer sr;
    
    public void Awake()
    {
        this.LoadComponents();
        Size = sr.transform.localScale.x * sr.sprite.textureRect.size.x / 2;
    }

    public void Init(PlanetInfo info)
    {
        transform.position = info.position;
        Size = info.size;
    }
}

[System.Serializable]
public struct PlanetInfo
{
    public Vector2 position;
    public float size;
}
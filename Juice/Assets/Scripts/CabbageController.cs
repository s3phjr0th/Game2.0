using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CabbageController : MonoBehaviour {

    private void OnCollide(Collider2D coll)
    {
        DestroyObject(gameObject);
    }

}

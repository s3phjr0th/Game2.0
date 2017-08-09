using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BananaTrigger : MonoBehaviour {
    private static int point = 0;
    public void OnTriggerEnter2D (Collider2D collision)
    {
        point++;
        gameObject.SetActive(false);
        Debug.Log("" + point);
    }

}

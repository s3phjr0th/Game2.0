using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4HintBar : MonoBehaviour {
    private Image image;

    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void OnPointerEnter() {
        Color imageColor = image.color;
        imageColor.a = 0;
        image.color = imageColor;
    }

    public void OnPointerExit() {
        Color imageColor = image.color;
        imageColor.a = 1;
        image.color = imageColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Level5ImgController : EventTrigger {
    private Vector2 mouseOffset;

    private void Start()
    {
        transform.localPosition = new Vector3(
            Random.Range(-277.5f, 277.5f),
            Random.Range(-194, 224),
            0
        );

        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.5f;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 canvasMouse = transform.parent.InverseTransformPoint(worldMouse);

        mouseOffset = canvasMouse - transform.localPosition;
    }

    public override void OnDrag(PointerEventData data)
    {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 canvasMouse = transform.parent.InverseTransformPoint(worldMouse);
        Vector2 expectedPosition = (Vector2)canvasMouse - mouseOffset;

        transform.localPosition = new Vector3(
            Mathf.Clamp(expectedPosition.x, -277.5f, 277.5f),
            Mathf.Clamp(expectedPosition.y, -294, 294),
            0
        );
    }
}

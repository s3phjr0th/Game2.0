using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    public float cameraSpeed;
    public Transform target;

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = target.position - transform.position;

            if (direction.magnitude <= cameraSpeed * Time.fixedDeltaTime)
            {
                transform.position += direction.WithZ(0);
            }
            else
            {
                transform.position += direction.ScaleTo(cameraSpeed * Time.fixedDeltaTime).WithZ(0);
            }
        }
    }
}

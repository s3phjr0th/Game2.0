using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour {
	public List<Transform> targets;
	public float distance;
    void LateUpdate () {

		transform.position = transform.position.WithX (targets.Max((target) => target.position.x) + distance);
	}
}

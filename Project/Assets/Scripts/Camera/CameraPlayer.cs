using UnityEngine;
using System.Collections;

public class CameraPlayer : MonoBehaviour {

	public Transform followTarget;
	public float cameraSmooth = 5f;

	Vector3 offset;

	void Start(){
		offset = transform.position - followTarget.position;
	}

	void FixedUpdate(){
		Vector3 camPos = followTarget.position + offset;
		//Move the camera using Lerp
		transform.position = Vector3.Lerp (transform.position, camPos, cameraSmooth * Time.deltaTime);
	}
}

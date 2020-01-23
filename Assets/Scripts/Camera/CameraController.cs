using UnityEngine;

public class CameraController : MonoBehaviour {

	private Transform player;
	private Vector3 target, mousepos, speed;
	private float cameraDist = 3.5f, smoothTime = 0.2f;

	private void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		target = player.position;
	}
	
	private void FixedUpdate () {
		mousepos = CaptureMousePos();
		target = UpdateTargetPos();
		UpdateCameraPosition();
	}

	private Vector3 CaptureMousePos(){
		Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		ret *= 2;
		ret -= Vector2.one;
		float max = 0.9f;
		if(Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
			ret = ret.normalized;
		
		return ret;
	}

	private Vector3 UpdateTargetPos(){
		Vector3 mouseOffset = mousepos * cameraDist;
		Vector3 ret = player.position + mouseOffset;
		ret.z = transform.position.z;
		return ret;
	}

	private void UpdateCameraPosition(){
		Vector3 tempPos;
		tempPos = Vector3.SmoothDamp(transform.position, target, ref speed, smoothTime);
		transform.position = tempPos;
	}
}

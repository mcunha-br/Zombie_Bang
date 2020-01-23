using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float m_xSpeed;
	public float m_ySpeed;
	public Vector2 m_minXAndY;
	public Vector2 m_maxXAndY;

	private Transform m_Player; 


	private void Awake(){
		m_Player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void FixedUpdate(){
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		targetX = Mathf.Lerp(transform.position.x, m_Player.position.x, m_xSpeed * Time.deltaTime);
		targetY = Mathf.Lerp(transform.position.y, m_Player.position.y, m_ySpeed * Time.deltaTime);

		targetX = Mathf.Clamp(targetX, m_minXAndY.x, m_maxXAndY.x);
		targetY = Mathf.Clamp(targetY, m_minXAndY.y, m_maxXAndY.y);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
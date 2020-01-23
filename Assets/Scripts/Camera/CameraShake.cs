using UnityEngine;

public class CameraShake : MonoBehaviour {

	public float m_shakeTime;
	public float m_shakePower;

	void Update () {
		if(m_shakeTime >= 0) {
			Vector2 shakePosition = Random.insideUnitCircle * m_shakePower;
			transform.position = new Vector3(transform.position.x + shakePosition.x, transform.position.y + shakePosition.y, transform.position.z);
			m_shakeTime -= Time.deltaTime;
		}
	}

	public void ShakeCamera(float _shakePower, float _shakeDuration){
		m_shakePower = _shakePower;
		m_shakeTime = _shakeDuration;
	}
}

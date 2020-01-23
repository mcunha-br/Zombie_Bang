using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject hitPrefab;

	private float speed;
	private int damage;

	private void Start () {
		Destroy (gameObject, 3.5f);
	}

	private void Update () {
		transform.Translate (Vector2.right * speed * Time.deltaTime);
	}

	public void SetValues (float speed, int damage) {
		this.speed = speed;
		this.damage = damage;
	}

	private void OnTriggerEnter2D (Collider2D collider) {
		if (collider.CompareTag ("Zombie")) 
			collider.GetComponent<ZombieBase> ().ApplyDamage (damage);
		
		if(collider.GetComponent<Money>() == null)
			Destroy (gameObject);
	}
}
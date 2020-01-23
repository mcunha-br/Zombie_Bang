using UnityEngine;

public class Money : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            GameController.Instance.Cash += 10;
            Destroy(gameObject);
        }
    }
}
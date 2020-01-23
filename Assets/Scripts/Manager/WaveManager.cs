using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    public ZombieStatus status;

    public Transform[] spawnPoints;
    public ZombieBase[] zombies;

    public IEnumerator SpawnEnemies () {
        for (int i = 0; i < (int) GameController.Instance.zombieForWave; i++) {
            var point = Random.Range (0, spawnPoints.Length);
            var enemy = Random.Range (0, zombies.Length);

            Instantiate (zombies[enemy], spawnPoints[point].position, Quaternion.identity);
            yield return new WaitForSeconds (0.5f);
        }
    }

    public void NextWave () {
        status.maxhealth *= 1.2f;
        status.speed *= 1.05f;
        status.strong *= 1.2f;
        status.attackRate -= 0.1f;
    }

    public void StartWave () {
        status.maxhealth = 5f;
        status.speed = 0.5f;
        status.strong = 5f;
        status.attackRate = 1f;
        status.distanceFollow = 50;
    }
}
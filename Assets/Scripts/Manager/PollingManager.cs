using UnityEngine;
using System.Collections.Generic;

public class PollingManager : MonoBehaviour {
    
    [Header("Settings Shoot")]
    public List<GameObject> objects;
    public GameObject bulletPrefab;
    public int shootAmount;

    private void Start() {
        objects.Polling(bulletPrefab, shootAmount, this.transform);        
    }
}
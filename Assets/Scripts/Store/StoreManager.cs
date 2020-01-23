using UnityEngine;
using System;

public class StoreManager : MonoBehaviour {

    public GameObject storeContent;
    public GameObject arrow;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            ActiveStore(true);
    }


    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player"))
            ActiveStore(false);
    }


    private void ActiveStore(bool value) {
        storeContent.SetActive(value);
        arrow.SetActive(!value);
    }

    public void EnableStore(bool value) {
        GetComponent<BoxCollider2D>().enabled = value;
        arrow.SetActive(value);
    }
}
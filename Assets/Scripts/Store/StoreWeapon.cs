using UnityEngine;
using UnityEngine.UI;

public class StoreWeapon : MonoBehaviour {
    public int cash;

    private Inventory inventory;
    private Button button;
    private bool adquired;

    private void Start () {
        inventory = FindObjectOfType<Inventory> ();
        button = GetComponent<Button>();
    }

    private void Update() {
        if(!adquired)
            button.interactable = (GameController.Instance.Cash >= cash);
    }

    public void BuyWeapon (Weapons weapons) {
        if(adquired) return;

        inventory.AddItems (weapons);
        GetComponent<Button>().interactable = false;
        transform.GetChild(0).gameObject.SetActive(true);
        GameController.Instance.Cash -= cash;
        // GameController.Instance.Cash();
        button.interactable = false ;
        adquired = true;
    }
}
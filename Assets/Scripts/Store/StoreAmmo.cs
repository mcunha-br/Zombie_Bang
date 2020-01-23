using UnityEngine;
using UnityEngine.UI;

public class StoreAmmo : MonoBehaviour {
    public Type ammoType;
    public int cash;

    private Inventory inventory;
    private Button button;

    private void Start () {
        inventory = FindObjectOfType<Inventory> ();
        button = GetComponent<Button> ();
    }

    private void Update () {
        button.interactable = (GameController.Instance.Cash >= cash);
    }

    public void BuyAmmo () {
        inventory.AddAmmo (ammoType);
        GameController.Instance.Cash -= cash;
        //GameController.Instance.Cash ();
        GameController.Instance.UpdateAmmoCurrent ();
    }
}
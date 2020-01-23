using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Type {
    WEAPON_PISTOL,
    WEAPON_AK47,
    WEAPON_UZI,
    WEAPON_12
}

public class Inventory : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public static Image icon;
    public static Weapons weapon;
    public static bool isItem;
    public static int index;

    public List<Weapons> inventory = new List<Weapons> ();

    private int countChild => transform.childCount;

    private void Awake () {
        for (int i = 0; i < countChild; i++) {
            inventory.Add (null);
            transform.GetChild (i).GetComponent<Slot> ().indexSlot = i;
        }
    }

    public void AddItems (Weapons weapon) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i] == null) {
                inventory[i] = weapon;
                break;
            }
        }
    }

    public void AddAmmo (Type type) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i] != null) {
                if (inventory[i].type == type) {
                    inventory[i].ammoTotal += inventory[i].shootForReload;
                    break;
                } 
            }
        }
    }

    public void OnPointerEnter (PointerEventData eventData) => GameController.Instance.swapWeapon = true;
    public void OnPointerExit (PointerEventData eventData) => GameController.Instance.swapWeapon = false;
}
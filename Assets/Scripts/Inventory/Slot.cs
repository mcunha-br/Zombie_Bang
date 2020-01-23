using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    public int indexSlot { get; set; }
    public KeyCode keyCode;

    private Image icon;
    private Inventory inv;
    private UIMouseControll uIMouse;
    private Weapon weapon;
    private HUDGamePlay hUDGamePlay;

    private void Start () {
        inv = FindObjectOfType<Inventory> ();
        uIMouse = FindObjectOfType<UIMouseControll> ();
        weapon = FindObjectOfType<Weapon>();
        hUDGamePlay = FindObjectOfType<HUDGamePlay>();

        icon = transform.GetChild (0).GetComponent<Image> ();
    }

    private void Update () {
        if (inv.inventory[indexSlot] != null) {
            icon.enabled = true;
            icon.sprite = inv.inventory[indexSlot].icon;   

        } else
            icon.enabled = false;

        if (Input.GetMouseButtonUp (0)) {
            if (!Inventory.isItem && uIMouse.mouseSlot.activeSelf) {
                inv.inventory[Inventory.index] = Inventory.weapon;
                DisableMouseSlot ();
            }
        }

        if(Input.GetKeyDown(keyCode) && inv.inventory[indexSlot] != null) {
            weapon.weapons = inv.inventory[indexSlot];
            hUDGamePlay.SetWeaponCurrent(weapon.weapons.icon, weapon.weapons.ammoAmout, weapon.weapons.ammoTotal);
            GameController.Instance.SetAnimatorController(weapon.weapons.runtimeAnimator);
        }
    }

    private void DisableMouseSlot () {
        Inventory.icon.sprite = null;
        Inventory.weapon = null;
        uIMouse.mouseSlot.SetActive (false);
    }

    public void GetItemSlot () {
        if (inv.inventory[indexSlot] != null) {
            Inventory.icon = uIMouse.mouseSlot.GetComponent<Image> ();
            Inventory.weapon = inv.inventory[indexSlot];
            Inventory.index = indexSlot;

            if (Inventory.icon.sprite == null) {
                uIMouse.mouseSlot.SetActive (true);
                Inventory.icon.sprite = Inventory.weapon.icon;
                inv.inventory[indexSlot] = null;
            }
        }
    }

    public void SetItemSlot () {
        if (inv.inventory[indexSlot] == null || Inventory.weapon == null) {
            if (uIMouse.mouseSlot.activeSelf) {
                inv.inventory[indexSlot] = Inventory.weapon;
                DisableMouseSlot ();
            }

        } else if (uIMouse.mouseSlot.activeSelf)
            SwapItems ();        
    }

    private void SwapItems () {
        inv.inventory[Inventory.index] = inv.inventory[indexSlot];
        inv.inventory[indexSlot] = Inventory.weapon;
        DisableMouseSlot ();
    }

    public void OnDrag (PointerEventData eventData) => GetItemSlot ();
    public void OnDrop (PointerEventData eventData) => SetItemSlot ();
    public void OnPointerEnter (PointerEventData eventData) => Inventory.isItem = true;
    public void OnPointerExit (PointerEventData eventData) => Inventory.isItem = false;
}
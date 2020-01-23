using UnityEngine;
using UnityEngine.UI;

public class HUDGamePlay : MonoBehaviour {
    public Slider sliderHealth;
    public Text txtZombiesAmout;
    public GameObject miniMap;

    public Image iconWeapon;
    public Text txtAmout;
    public Text txtMoney;
    public Text txtAmmoTotal;

    private GameController gameController => GameController.Instance;

    public void UpdateAmmo (int amout, int totalAmmo) {
        txtAmout.text = "x " + amout;
        txtAmmoTotal.text = "/" + totalAmmo;
    }

    public void UpdateHealth (float value) {
        sliderHealth.value = value;
    }

    public void UpdateZombiesAmount (int death, int amout) {
        txtZombiesAmout.text = string.Format ("Dead Zombies: {0}/{1}", death, amout);
    }

    public void SelectWeapon (Weapons weapons) {
        gameController.SetWeapon (weapons);
    }

    public void SetAnimatorController (RuntimeAnimatorController animatorController) {
        gameController.SetAnimatorController (animatorController);
    }

    public void SwapWeapon (bool value) {
        gameController.swapWeapon = value;
    }

    public void SetWeaponCurrent (Sprite image, int amout, int totalAmmo) {
        iconWeapon.sprite = image;
        txtAmout.text = "x " + amout;
        txtAmmoTotal.text = "/" + totalAmmo;
    }

    public void CoultdownNextWave (int value) {
        txtZombiesAmout.text = string.Format ("Next Wave: {0}", value);
    }

    public void UpdateMoney (int value) {
        txtMoney.text = string.Format ("Money: {0}", value);
    }
}
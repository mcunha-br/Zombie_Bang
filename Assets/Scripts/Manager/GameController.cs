using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    public float zombieForWave;
    public bool swapWeapon { get; set; }
    public bool canWalk { get; set; }

    private HUDGamePlay hUDGamePlay;
    private Weapon weapon;
    private Animator playerAnim;
    private ZombieBase[] allZombies;
    private Animator hudAnimator;
    private WaveManager waveManager;
    private StoreManager storeManager;
    private int zombiesDeath;
    private int cash;

    public int ZombiesDeath {
        get { return zombiesDeath; }
        set {
            zombiesDeath = value;
            hUDGamePlay.UpdateZombiesAmount (zombiesDeath, (int) zombieForWave);
        }
    }

    public int Cash {
        get { return cash; }
        set {
            cash = value;
            hUDGamePlay.UpdateMoney (cash);
        }
    }

    private void Awake () {
        if (Instance == null)
            Instance = this;

        hUDGamePlay = FindObjectOfType<HUDGamePlay> ();
        waveManager = FindObjectOfType<WaveManager> ();
        weapon = FindObjectOfType<Weapon> ();
        playerAnim = GameObject.FindWithTag ("Player").GetComponent<Animator> ();
        hudAnimator = hUDGamePlay.GetComponent<Animator> ();
        storeManager = FindObjectOfType<StoreManager> ();

        StartCoroutine (Wave (() => { waveManager.StartWave (); }));
        Cash = 1000;
    }

    public void UpdateAmmo (int ammo, int ammoTotal) {
        hUDGamePlay.UpdateAmmo (ammo, ammoTotal);
    }

    public void UpdateHealth (float value) {
        hUDGamePlay.UpdateHealth (value);
    }

    public void UpdateZombies () {
        ZombiesDeath++;

        if (ZombiesDeath >= (int) zombieForWave) {
            StartCoroutine (Wave (() => {
                zombieForWave *= 1.2f;
                waveManager.NextWave ();
            }));
        }
    }

    public void SetWeapon (Weapons weapon) {
        this.weapon.weapons = weapon;
        this.weapon.currentFireRate = 0;
        UpdateAmmo (weapon.ammoAmout, weapon.ammoTotal);
    }

    public void SetAnimatorController (RuntimeAnimatorController animatorController) {
        playerAnim.runtimeAnimatorController = animatorController;
    }

    public void Fade () {
        hudAnimator.SetTrigger ("FADE");
    }

    public void WeaponInUse (bool value) {
        swapWeapon = value;
    }


    private IEnumerator Wave (Action action) {
        int timer = 15;
        storeManager.EnableStore (true);

        while (timer >= 0) {
            hUDGamePlay.CoultdownNextWave (timer);
            timer--;
            yield return new WaitForSeconds (1);
        }

        if (action != null)
            action ();

        ZombiesDeath = 0;
        storeManager.EnableStore (false);
        StartCoroutine (waveManager.SpawnEnemies ());
    }

    public void UpdateAmmoCurrent () => weapon.UpdateAmmotCurrent ();
}
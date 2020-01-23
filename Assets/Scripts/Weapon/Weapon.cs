using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Weapons weapons;
	public Transform barrel;
	public float currentFireRate { get; set; }

	private AudioSource audioSource;
	private PlayerController playerController;
	private CameraShake cameraShake;
	private bool canShoot;
	private bool inReload;
	private float timerForWalk;
	private GameController gameController;

	protected void Start () {
		audioSource = GetComponent<AudioSource> ();
		playerController = GetComponentInParent<PlayerController> ();
		cameraShake = FindObjectOfType<CameraShake> ();
		gameController = GameController.Instance;
		weapons.ammoAmout = weapons.shootForReload;
	}

	protected void Update () {
		currentFireRate += Time.deltaTime;
		canShoot = (currentFireRate >= weapons.fireRate);

		timerForWalk += Time.deltaTime;
		gameController.canWalk = (timerForWalk >= weapons.fireRate);

		if (Input.GetMouseButton (0) || Input.GetKey (KeyCode.J))
			Shoot ();
	}

	public void Shoot () {
		if (canShoot && !inReload && !gameController.swapWeapon) {
			if (weapons.ammoAmout > 0) {
				weapons.ammoAmout--;
				gameController.UpdateAmmo (weapons.ammoAmout, weapons.ammoTotal);

				currentFireRate = 0;
				playerController.Animations ("shoot");
				audioSource.PlayOneShot (weapons.shoot, .5f);
				cameraShake.ShakeCamera (weapons.shakePower, weapons.shakeDuration);

				Shooting ();

				if (weapons.type == Type.WEAPON_UZI)
					Invoke ("Shooting", 0.2f);

			} else
				StartCoroutine (Reload ());
		}
		timerForWalk = 0;
	}

	private IEnumerator Reload () {
		inReload = true;
		playerController.Animations ("reload");
		audioSource.PlayOneShot (weapons.reload, .2f);

		if (weapons.type == Type.WEAPON_PISTOL) {
			weapons.ammoAmout = weapons.shootForReload;

		} else {
			if (weapons.ammoTotal > 0) {
				if (weapons.ammoTotal >= weapons.shootForReload) {
					weapons.ammoTotal -= weapons.shootForReload;
					weapons.ammoAmout = weapons.shootForReload;

				} else {
					weapons.ammoAmout = weapons.ammoTotal;
					weapons.ammoTotal = 0;
				}
			}
		}

		if (weapons.type == Type.WEAPON_UZI) {
			yield return new WaitForSeconds (0.2f);
			audioSource.PlayOneShot (weapons.reload, .2f);
		}

		gameController.UpdateAmmo (weapons.ammoAmout, weapons.ammoTotal);
		yield return new WaitForSeconds (1);
		inReload = false;
	}

	private void Shooting () {
		GameObject tempBullet = Instantiate (weapons.bulletPrefab, barrel.transform.position + Random.insideUnitSphere * weapons.shakeAmmo, barrel.transform.rotation);
		tempBullet.GetComponent<Bullet> ().SetValues (weapons.fireSpeed, weapons.damage);
	}

	public void SetWeapon (Weapons weapon) {
		weapons = weapon;
	}

	public void UpdateAmmotCurrent () => gameController.UpdateAmmo (weapons.ammoAmout, weapons.ammoTotal);
}
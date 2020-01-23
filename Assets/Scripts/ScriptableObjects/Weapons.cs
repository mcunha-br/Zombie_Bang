using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Geta Game Jam 10/Weapon", order = 0)]
public class Weapons : ScriptableObject {
    [Header("Settings Weapon")]
    public Sprite icon;
    [Range(0,02)] public float fireRate;
    [Range(0,20)] public float fireSpeed;
	[Range(0,30)] public int damage;
    [Range(0,30)] public int shootForReload;
	[Range(0,05)] public float shakePower;
    [Range(0,05)] public float shakeDuration;
    [Range(0,05)] public float shakeAmmo;

    public RuntimeAnimatorController runtimeAnimator;

    public Type type;

    public int ammoAmout;
    public int ammoTotal;

    [Header("Settings Audio")]
	public AudioClip shoot;
    public AudioClip reload;
    public AudioClip melee;

    [Header("Settings Prefabs")]
	public GameObject bulletPrefab;
	public GameObject flarePrefab;    
}
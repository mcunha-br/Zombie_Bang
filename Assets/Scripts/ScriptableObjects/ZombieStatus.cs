using UnityEngine;

[CreateAssetMenu (fileName = "ZombieStatus", menuName = "Geta Game Jam 10/ZombieStatus", order = 0)]
public class ZombieStatus : ScriptableObject {    
    public float maxhealth;
    public float speed;
    public float strong;
    public float distanceFollow;
    public float attackRate;
    public GameObject blood;
    public GameObject particles;
    public GameObject money;

    public AudioClip attack;
    public AudioClip die;
    public AudioClip hit;
}
using System.Collections;
using UnityEngine;

public abstract class ZombieBase : MonoBehaviour {

    public ZombieStatus status;

    [HideInInspector] public Transform target;

    protected float speedCurrent;
    protected float currentDistanceFollow;
    protected float distance;
    protected float timer;
    protected bool canFollow = true;
    protected bool canAttack = true;
    protected Transform player;
    protected PlayerController playerController;
    protected SpriteRenderer sprite;

    protected Animator anim;
    protected Rigidbody2D body;
    protected float health;
    protected bool lookRight = true;
    protected AudioSource source;
    protected bool death;

    protected void Start () {
        anim = GetComponent<Animator> ();
        body = GetComponent<Rigidbody2D> ();
        sprite = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        health = status.maxhealth;

        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        currentDistanceFollow = status.distanceFollow;
        target = player;
        speedCurrent = status.speed;
        Flip();
    }

    protected void Update () {
        if(playerController.death) return;

        distance = Vector2.Distance (target.position, transform.position);
        status.distanceFollow = (target == player) ? currentDistanceFollow : currentDistanceFollow * 2;

        if(transform.position.x > player.position.x && !lookRight || transform.position.x < player.position.x && lookRight )
            Flip();

        OnUpdate ();
    }

    public void ApplyDamage (float _damage) {
        health -= _damage;
        if (health <= 0) {
            health = 0;
            GameController.Instance.UpdateZombies();
            OnDied ();
        }
        
        StartCoroutine (DamageBlink ());
        OnDamage ();
    }

    private IEnumerator DamageBlink () {        
        sprite.color = Color.red;
        yield return new WaitForSeconds (.1f);
        sprite.color = Color.white;
    }

    private void Flip() {
        lookRight = !lookRight;
        sprite.flipX = lookRight;
    }

    protected abstract void OnUpdate ();
    protected abstract void OnDamage ();
    protected abstract void OnDied ();
}
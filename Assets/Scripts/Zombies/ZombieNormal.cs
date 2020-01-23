using System.Collections;
using UnityEngine;

public class ZombieNormal : ZombieBase {

    protected override void OnUpdate () {
        timer += Time.deltaTime;

        if (distance <= status.distanceFollow && canFollow && !death) {
            speedCurrent = status.speed;

            transform.position = Vector2.MoveTowards (transform.position, target.position, speedCurrent * Time.deltaTime);
        } else
            speedCurrent = 0;


        if (timer >= status.attackRate && !canFollow && !death) {
                timer = 0;                
                source.PlayOneShot(status.attack, 0.3f);
                anim.Play("Attack");
            }
    }

    protected override void OnDamage () {
        //Instantiate (status.particles, transform.position, Quaternion.identity);
        source.PlayOneShot(status.hit, 0.3f);
    }

    protected override void OnDied () {
        //Instantiate (status.blood, transform.position, transform.rotation);
        Instantiate(status.money, transform.position, Quaternion.identity);
        anim.Play("Death");
        source.PlayOneShot(status.die, 0.3f);
        death = true;
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy (gameObject, 1);
    }

    private void OnCollisionStay2D (Collision2D other) {
        if (other.gameObject.CompareTag ("Player")) 
            canFollow = false;        
    }

    private void OnCollisionExit2D (Collision2D other) {
        if (other.gameObject.CompareTag ("Player")) 
            canFollow = true;        
    }

    public void Attack() {
        player.GetComponent<PlayerController>().ApplyDamage (status.strong);
    }
}
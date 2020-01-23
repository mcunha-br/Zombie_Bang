using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float health;
    public float speed;
    public Vector2 minXAndY, maxXAndY;
    public bool death {get; private set; } = false;

    private float xPos, yPos;
    private Animator anim;
    private Rigidbody2D body;
    private float maxHealth;
    private bool lookRight = true;

    private GameController gameController;
    

    private void Start () {
        anim = GetComponent<Animator> ();
        body = GetComponent<Rigidbody2D> ();
        gameController = GameController.Instance;
        maxHealth = health;

        gameController.UpdateHealth (health);
    }

    private void Update () {
        if (!gameController.canWalk) {
            xPos = 0;
            yPos = 0;
            anim.SetFloat ("velocity", 0);
            return;
        }

        xPos = Input.GetAxisRaw ("Horizontal");
        yPos = Input.GetAxisRaw ("Vertical");

        if (xPos > 0 && !lookRight || xPos < 0 && lookRight)
            Flip ();

        var setAnim = (xPos != 0 || yPos != 0) ? 1 : 0;
        anim.SetFloat ("velocity", setAnim);

        ClampPosition ();
    }

    private void FixedUpdate () {
        Vector2 moviment = new Vector2 (xPos, yPos).normalized * speed;
        body.velocity = moviment;
    }

    private void ClampPosition () {
        var clampPos = transform.position;
        clampPos.x = Mathf.Clamp (clampPos.x, minXAndY.x, maxXAndY.x);
        clampPos.y = Mathf.Clamp (clampPos.y, minXAndY.y, maxXAndY.y);

        transform.position = clampPos;
    }

    public void GetHealth (float health) {
        float regen = this.health + health;
        this.health = (regen >= maxHealth) ? maxHealth : regen;

        gameController.UpdateHealth (this.health);
    }

    public void ApplyDamage (float _damage) {
        health -= _damage;
        if (health <= 0 && !death) {
            death = true;
            health = 0;
            Animations("death");
            Invoke("GameOver", 2);
        }
        gameController.UpdateHealth (health);
    }    

    public void SetMinXAndY (Vector2 minXAndY, Vector2 maxXAndY) {
        this.minXAndY = minXAndY;
        this.maxXAndY = maxXAndY;
    }

    private void Flip () {
        lookRight = !lookRight;
        transform.right = transform.right * -1;
    }

    public void Animations (string animation) => anim.SetTrigger (animation);
    private void GameOver() => SceneManager.LoadScene("GameOver");
    
}
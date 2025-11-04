using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    public int damage = 50; // Damage dealt by the projectile


    public Vector2 knockback = new Vector2(0, 0); // Knockback effect applied to the target

    public Vector2 moveSpeed = new Vector2(3f, 0); // Speed of the projectile

    Rigidbody2D rb; // Reference to the Rigidbody2D component

    public float lifeTime = 8f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to this GameObject
    }

    private void Start()
    {
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y); // Set the linear velocity of the Rigidbody2D to move the projectile


        Destroy(gameObject, lifeTime); // Destroy the proje ctile after a certain time
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>(); // Get the Damageable component from the collided object

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);


            bool gotHit = damageable.Hit(damage, deliveredKnockback);

            if (gotHit)
            {


                Debug.Log(collision.name + " hit for " + damage);

                Destroy(gameObject); // Destroy the projectile after it hits a target
            }
        }
    }
}


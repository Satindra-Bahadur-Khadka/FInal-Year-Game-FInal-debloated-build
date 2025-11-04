using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    [SerializeField]
    public int healthRestore = 20;


    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);


    AudioSource pickupSource;



    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            bool wasHealed = damageable.Heal(healthRestore);


            if (wasHealed)
            {
                if(pickupSource)
                {
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                }
                Destroy(gameObject);
            }
            
        }
    }


    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}

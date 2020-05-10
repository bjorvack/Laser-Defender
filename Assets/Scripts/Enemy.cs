using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Projectile")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] bool shoots = true;

    [Header("Explosion")]
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float deathDuration = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1f)] float volume = 0.75f;

    [Header("Shoot sound")]
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1F)] float shootVolume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        if (shoots)
        {
            CountDownAndShoot();
        }
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {

        GameObject laser = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        ) as GameObject;
        AudioSource.PlayClipAtPoint(
              shootSFX,
              Camera.main.transform.position,
              shootVolume
          );

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, - projectileSpeed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) return;

        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, deathDuration);
        AudioSource.PlayClipAtPoint(
            deathSFX,
            Camera.main.transform.position,
            volume
        );
        FindObjectOfType<GameSession>().AddScore(scoreValue);
    }
}

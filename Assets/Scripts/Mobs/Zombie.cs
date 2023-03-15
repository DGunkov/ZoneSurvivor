using UnityEngine;

public class Zombie : Units_Standart
{
    [SerializeField] private AudioSource step_sound;
    private Transform player;
    private Rigidbody2D rb;

    private float time_to_step = 0;

    [SerializeField] private float rotate_speed;

    [SerializeField] private float distance_attack;

    private bool attack_ready = true;

    private bool attack_b = true;

    [SerializeField] internal int damage;

    float distance()
    {
        float dist = Vector2.Distance(transform.position, player.transform.position);
        return dist;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Attack()
    {
        if(distance() < distance_attack * 1.2f)
        {
            player.gameObject.GetComponent<TakeDamage>().Damage(damage, "bite", 0);
        }
        attack_ready = true;
    }

    void Update()
    {
        
        float angle = Vector2.Angle(transform.up, player.position - transform.position);

        
        if (((angle < 45 && distance() < see_distance) || attack_b) && distance() > distance_attack)
        {
            if (time_to_step > 0)
            {
                time_to_step -= Time.deltaTime;
            }
            else
            {
                time_to_step = 0.7f;
                step_sound.pitch = Random.Range(0.5f, 0.9f);
                step_sound.Play();
            }
            attack_b = true;

            rb.velocity = transform.up * Time.deltaTime * move_speed;
        }
        else
        {
            if(distance() < distance_attack && attack_ready)
            {
                attack_ready = false;
                Invoke("Attack", 1);
            }
            rb.velocity = new Vector2(0, 0);
        }
        if (attack_b)
        {
            Vector2 direction = player.transform.position - transform.position;
            transform.up = Vector2.MoveTowards(transform.up, direction, Time.deltaTime * rotate_speed);
        }
    }
}

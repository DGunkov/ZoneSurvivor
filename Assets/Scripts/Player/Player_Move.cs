using UnityEngine;

public class Player_Move : MonoBehaviour
{
    [SerializeField] private AudioSource step_sound;
    private Rigidbody2D rb;

    [SerializeField, Range(1, 500)] private float move_speed_standart;
    [SerializeField, Range(1, 10)] private float sprint_factor;
    [SerializeField, Range(0.01f, 1)] private float reverse_factor;
    private float move_speed;
    internal float health_factor = 1;


    [SerializeField, Range(0.01f, 5)] private float standart_time_step = 0.6f;
    private float time_to_step = 0;

    Vector2 direction_move()
    {
        Vector2 dir_move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        return dir_move;
    }

    void Start()
    {
        time_to_step = standart_time_step;
        rb = GetComponent<Rigidbody2D>();
    }    

    void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Vector2.Angle(direction, direction_move());

            move_speed = move_speed_standart;
            if (Input.GetKey(KeyCode.LeftShift) && angle < 60 && Input.GetAxis("Fire1") == 0 && Input.GetAxis("Fire2") == 0)
            {
                move_speed *= sprint_factor;
            }
            else
            {
                if (angle > 90)
                {
                    move_speed *= reverse_factor;
                }
            }

            
        }

        if (time_to_step > 0)
        {
            time_to_step -= Time.deltaTime * rb.velocity.magnitude;
        }
        else
        {
            time_to_step = standart_time_step;
            step_sound.pitch = Random.Range(0.5f, 0.9f);
            step_sound.Play();
        }

        rb.velocity = Vector2.Lerp(rb.velocity, direction_move() * move_speed * health_factor, Time.deltaTime * 3);
    }
}

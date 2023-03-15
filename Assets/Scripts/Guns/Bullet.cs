using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private int move_speed;
    private int damage;
    public void SetBulletStats(int speed_for_gun, int damage_for_gun)
    {
        move_speed = speed_for_gun;
        damage = damage_for_gun;
    }

    void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.TryGetComponent(out Units_Standart Units_Standart))
        {
            Units_Standart.Damage(damage);
        }
        /*if (obj.gameObject.TryGetComponent(out Player_Stats Player_Stats))
        {
            Player_Stats.Damage(damage);
        }*/
        DestroyBullet();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyBullet", 2);
    }

    void Update()
    {
        rb.velocity = transform.up * move_speed * Time.deltaTime * 10;
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

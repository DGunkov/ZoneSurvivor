using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    [SerializeField] private Transform spawn_for_bullet;
    [SerializeField] private SpriteRenderer gun_sprite;
    [SerializeField] private Transform slot_for_gun_1;
    [SerializeField] private Transform slot_for_gun_2;


    [SerializeField] private GameObject bullet_prefab;
    [SerializeField] private GameObject shoot_light_prefab;

    [SerializeField] private AudioSource shoot_sound;
    [SerializeField] private AudioSource reload_sound;
    [SerializeField] private AudioSource zero_ammo_sound;

    [SerializeField] Gun[] giving_guns;
    private Gun keeps_gun;
    private Weapon_Item check_weapon;


    private float spread_angle;
    private float stabilization;
    private float return_force;
    private float accuracy;
    private float time_to_new_bullet = 0;
    private float reloading_time;

    private int rate_of_fire;
    private int max_ammo;
    private int select_gun_number;

    internal bool reloading;
    private bool keeps_gun_b = false;
    private bool give_other_gun;

    internal float health_factor = 1;


    private float angle_for_bullet()
    {
        float angle = spread_angle;
        angle += accuracy;
        if (Input.GetAxis("Fire2") > 0)
        {
            angle /= 3;
        }
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            angle *= 1.2f;
        }
        return angle;
    }
    private float fixed_time_to_new_bullet()
    {
        float x = 60f / rate_of_fire;
        return x;
    }
    internal void ClearGun()
    {
        if (select_gun_number != 0)
        {
            keeps_gun = null;
            select_gun_number = 0;
            keeps_gun_b = false;
            gun_sprite.sprite = null;
        }
    }

    void TakeGun()
    {
        keeps_gun_b = true;
        give_other_gun = false;
        gun_sprite.sprite = keeps_gun.Gun_sprite;
        rate_of_fire = keeps_gun.Rate_of_fire;
        spread_angle = keeps_gun.Spread_angle;
        stabilization = keeps_gun.Stabilization;
        return_force = keeps_gun.Return_force;
        max_ammo = keeps_gun.Max_ammo;
        reloading_time = keeps_gun.Reloading_time;

        shoot_sound.clip = keeps_gun.Shoot_sound;
        reload_sound.clip = keeps_gun.Reload_sound;
    }

    private void StartTakeGun(Transform slot)
    {
        if (slot.GetComponentInChildren<Item>() != null)
        {
            check_weapon = slot.GetComponentInChildren<Weapon_Item>();
            if (keeps_gun != check_weapon.gun)
            {
                keeps_gun_b = false;
                give_other_gun = true;
                keeps_gun = check_weapon.gun;
                Invoke("TakeGun", keeps_gun.Time_to_give);
            }
        }
    }
    void FixedUpdate()
    {
        if(!give_other_gun)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(select_gun_number != 1)
                {
                    ClearGun();
                    select_gun_number = 1;
                    StartTakeGun(slot_for_gun_1);                    
                }
                else
                {
                    ClearGun();
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (select_gun_number != 2)
                {
                    ClearGun();
                    select_gun_number = 2;
                    StartTakeGun(slot_for_gun_2);
                }
                else
                {
                    ClearGun();
                }
            }
        }
        
        if (keeps_gun_b)
        {
            if (accuracy > 0)
            {
                accuracy -= Time.deltaTime * stabilization / 20;
            }
            else
            {
                accuracy = 0;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && check_weapon.ammo == 0 && !reloading)
            {
                zero_ammo_sound.PlayOneShot(zero_ammo_sound.clip);
            }

            if (Input.GetKeyDown(KeyCode.R) && !reloading)
            {
                reloading = true;
                reload_sound.Play();
                Invoke("Reload", reloading_time);
            }

            if (time_to_new_bullet > 0)
            {
                time_to_new_bullet -= Time.deltaTime;
            }
            else
            {
                if (Input.GetAxis("Fire1") > 0 && check_weapon.ammo > 0 && !reloading)
                {
                    Shoot();
                }
            }
        }      
    }
    void Shoot()
    {
        accuracy += return_force / 100;

        shoot_sound.pitch = Random.Range(0.7f, 0.9f);
        shoot_sound.PlayOneShot(shoot_sound.clip);

        GameObject bullet_obj = Instantiate(bullet_prefab, spawn_for_bullet.position, transform.rotation);
        GameObject light = Instantiate(shoot_light_prefab, spawn_for_bullet);
        bullet_obj.GetComponent<Bullet>().SetBulletStats(keeps_gun.Move_speed, keeps_gun.Damage);
        float angle = angle_for_bullet();
        angle /= health_factor;
        bullet_obj.transform.eulerAngles += new Vector3(0, 0, Random.Range(-angle, angle));

        light.transform.position -= new Vector3(0, 0, 0.1f);

        time_to_new_bullet = fixed_time_to_new_bullet();
        check_weapon.ammo--;
    }

    void Reload()
    {
        check_weapon.ammo = max_ammo;
        reloading = false;
    }
}

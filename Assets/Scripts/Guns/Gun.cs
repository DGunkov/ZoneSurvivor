using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Custom/Gun")]
public class Gun : ScriptableObject
{
    public Vector2Int size;
    [SerializeField] string item_name;
    [SerializeField] string description;
    [SerializeField] private AudioClip shoot_sound;
    [SerializeField] private AudioClip reload_sound;
    [SerializeField] private Sprite gun_sprite;
    [SerializeField, Range(1, 1000)] private int rate_of_fire;
    [SerializeField, Range(1, 500)] private int max_ammo;
    [SerializeField, Range(1, 500)] private int move_speed;
    [SerializeField, Range(1, 1000)] private int damage;
    [SerializeField, Range(0, 500)] private float spread_angle;
    [SerializeField, Range(1, 500)] private float stabilization;
    [SerializeField, Range(0, 500)] private float return_force;
    [SerializeField, Range(0.01f, 60)] private float reloading_time;
    [SerializeField, Range(0.01f, 50)] private float time_to_give;

    internal int Max_ammo { get => max_ammo; }
    internal int Rate_of_fire { get => rate_of_fire; }
    internal int Move_speed { get => move_speed; }
    internal int Damage { get => damage; }
    internal float Spread_angle { get => spread_angle; }
    internal float Stabilization { get => stabilization; }
    internal float Return_force { get => return_force; }
    
    internal float Reloading_time { get => reloading_time; }
    internal float Time_to_give { get => time_to_give; }
    internal Sprite Gun_sprite { get => gun_sprite; }
    internal AudioClip Shoot_sound { get => shoot_sound; }
    internal AudioClip Reload_sound { get => reload_sound; }
}

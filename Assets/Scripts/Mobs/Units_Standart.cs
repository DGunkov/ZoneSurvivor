using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units_Standart : MonoBehaviour
{
    [SerializeField, Range(1, 500)]
    protected int move_speed;

    [SerializeField, Range(0.01f, 500)]
    protected float see_distance;

    [SerializeField, Range(1, 500)]
    private int hp;

    internal void Damage(int dmg)
    {
        hp -= dmg;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}

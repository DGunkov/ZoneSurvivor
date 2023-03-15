using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Item : MonoBehaviour
{
    public Gun gun;
    internal int ammo;

    void Start()
    {
        ammo = gun.Max_ammo;
    }
}

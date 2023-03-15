using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject zombie_prefab;

    [SerializeField]
    private float time_to_new_zombie = 2f;

    void Update()
    {
        if(time_to_new_zombie > 0)
        {
            time_to_new_zombie -= Time.deltaTime;
        }
        else
        {
            float randAng = Random.Range(0, Mathf.PI * 2);
            Vector2 randOnCircle = new Vector2(Camera.main.transform.position.x + Mathf.Cos(randAng) * 20, Camera.main.transform.position.y + Mathf.Sin(randAng) * 20);

            Instantiate(zombie_prefab, randOnCircle, zombie_prefab.transform.rotation);
            time_to_new_zombie = 1f;
        }
    }
}

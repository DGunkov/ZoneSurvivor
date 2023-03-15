using UnityEngine;

public class Shoot_Flash : MonoBehaviour
{
    void Start()
    {
        Invoke("Off", 0.07f);
    }
    void Off()
    {
        Destroy(gameObject);
    }
}

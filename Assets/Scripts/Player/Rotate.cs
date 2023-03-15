using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] private float rotate_speed;
    void Update()
    {
        Vector2 direction_move = transform.up;
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Vector2.SignedAngle(direction_move, direction);
        Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.Rotate(0, 0, angle * Time.deltaTime * (rotate_speed / 100));
    }
}

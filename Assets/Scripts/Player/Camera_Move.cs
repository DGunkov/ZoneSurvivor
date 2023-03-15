using UnityEngine;

public class Camera_Move : MonoBehaviour
{

    private Camera camera_component;

    private Transform player;
    [SerializeField] private Transform place_for_camera;
    [SerializeField] private GameObject aim;

    private float last_mouse_distance;

    private Fire fire_script;

    internal float health_factor = 1;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
    }

    private void Start()
    {
        camera_component = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fire_script = player.GetComponent<Fire>();
    }
    void LateUpdate()
    {
        Vector3 direction;
        direction = player.position + new Vector3(0, 0, -10);
        transform.position = Vector3.MoveTowards(transform.position, direction, 50f * Time.deltaTime);

        Vector3 mouse_position_in_screen = new Vector3(Screen.width, Screen.height) / 2 - Input.mousePosition;
        mouse_position_in_screen = new Vector3(mouse_position_in_screen.x, mouse_position_in_screen.y * 2);
        float distance_to_mouse = Vector3.Distance(mouse_position_in_screen, new Vector3(0, 0));

        if (Input.GetAxis("Fire2") > 0 && !fire_script.reloading)
        {
            aim.SetActive(true);

            Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aim.transform.position = Vector3.MoveTowards(aim.transform.position, mouse_position + new Vector3(0, 0, 2), 30 * Time.deltaTime);
            
            if (distance_to_mouse > 500 && distance_to_mouse < 950)
            {
                last_mouse_distance = distance_to_mouse;
                camera_component.orthographicSize = Mathf.Lerp(camera_component.orthographicSize, distance_to_mouse / 190, Time.deltaTime * 10);
            }
            else
            {
                if (last_mouse_distance < 500)
                {
                    last_mouse_distance = 500;
                }
                else
                {
                    if (last_mouse_distance > 950)
                    {
                        last_mouse_distance = 950;
                    }
                }
                camera_component.orthographicSize = Mathf.Lerp(camera_component.orthographicSize, last_mouse_distance / 190 * health_factor, Time.deltaTime * 10);
            }
        }
        else
        {
            aim.SetActive(false);
            aim.transform.position = player.position;
            camera_component.orthographicSize = Mathf.Lerp(camera_component.orthographicSize, 5, Time.deltaTime * 10);
        }
    }
}

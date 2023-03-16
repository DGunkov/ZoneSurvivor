using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controll : MonoBehaviour
{
    [SerializeField] GameObject inventory_panel;
    [SerializeField] GameObject health_panel;
    [SerializeField] GameObject death_panel;
    private GameObject player;
    private Fire fire_script;
    private Rotate rotate_script;
    private Player_Move move_script;
    private Camera_Move camera_script;
    internal Item drag_item;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fire_script = player.GetComponent<Fire>();
        rotate_script = player.GetComponent<Rotate>();
        move_script = player.GetComponent<Player_Move>();
        camera_script = Camera.main.GetComponent<Camera_Move>();
    }

    internal void Death()
    {
        death_panel.SetActive(true);
        death_panel.GetComponent<AudioSource>().Play();
        inventory_panel.SetActive(false);
        Cursor.visible = true;
        fire_script.enabled = false;
        rotate_script.enabled = false;
        move_script.move_ready = false;
        camera_script.move_ready = false;
    }
    private void SwitchInventory()
    {
        if(drag_item != null)
        {
            drag_item.ReturnPosition();
            drag_item = null;
        }
        bool active_inventory = inventory_panel.activeSelf;
        inventory_panel.SetActive(!active_inventory);
        health_panel.SetActive(active_inventory);
        Cursor.visible = !active_inventory;
        fire_script.enabled = active_inventory;
        rotate_script.enabled = active_inventory;
        move_script.move_ready = active_inventory;
        camera_script.move_ready = active_inventory;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchInventory();
        }
    }
}

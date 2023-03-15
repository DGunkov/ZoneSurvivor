using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controll : MonoBehaviour
{
    [SerializeField] GameObject inventory_panel;
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

    private void SwitchInventory()
    {
        if(drag_item != null)
        {
            drag_item.ReturnPosition();
            drag_item = null;
        }
        bool actvie_inventory = inventory_panel.activeSelf;
        inventory_panel.SetActive(!actvie_inventory);
        Cursor.visible = !actvie_inventory;
        fire_script.enabled = actvie_inventory;
        rotate_script.enabled = actvie_inventory;
        move_script.enabled = actvie_inventory;
        camera_script.enabled = actvie_inventory;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchInventory();
        }
    }
}

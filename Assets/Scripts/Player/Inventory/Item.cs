using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    private Fire fire_script;
    private CanvasGroup canvas_group;
    private RectTransform rect_transform;

    private bool give_new_parent;
    private bool take_item;
    internal bool rotate;

    public Vector2Int size;

    private UI_Controll ui_script;

    public List<Slot> occupied_slots;
    private void Start()
    {
        ui_script = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UI_Controll>();
        fire_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Fire>();
        canvas_group = GetComponent<CanvasGroup>();
        rect_transform = GetComponent<RectTransform>();
        foreach(Slot cell in occupied_slots)
        {
            cell.full = true;
        }
        ParentSetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(eventData.position).x, Camera.main.ScreenToWorldPoint(eventData.position).y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        foreach (Slot cell in occupied_slots)
        {
            cell.full = false;
        }
        ui_script.drag_item = this;
        occupied_slots.Clear();
        ParentSetAsLastSibling();

        take_item = true;
        canvas_group.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnPosition();
    }

    internal void ReturnPosition()
    {
        if (!give_new_parent)
        {
            ReturnRotation();
        }
        ui_script.drag_item = null;
        give_new_parent = false;
        take_item = false;
        canvas_group.blocksRaycasts = true;
        rect_transform.localPosition = Vector3.zero;
        ParentSetAsLastSibling();
    }

    private void ParentSetAsLastSibling()
    {
        for (var parent = rect_transform.parent; parent.name != "UI"; parent = parent.parent)
        {
            parent.SetAsLastSibling();
        }
    }
    internal void ReturnRotation()
    {
        if (rotate)
        {
            Rotate();
        }
        rotate = false;
    }
    internal void SetNewParent(Transform new_parent)
    {
        give_new_parent = true;
        if (transform.parent.TryGetComponent(out Slot_For_Gun other_slot))
        {
            fire_script.ClearGun();
            other_slot.full = false;
        }
        transform.SetParent(new_parent);

        rotate = false;
    }
    internal void Rotate()
    {
        int x = size.x;
        size.x = size.y;
        size.y = x;
        if (transform.rotation.z == 0)
        {
            transform.Rotate(0, 0, -90);
        }
        else
        {
            transform.Rotate(0, 0, 90);
        }
    }
    private void Update()
    {
        if(take_item)
        {
            if (Input.GetKeyDown(KeyCode.R) && size.x != size.y)
            {
                rotate = true;
                Rotate();
            }
        }
    }
}

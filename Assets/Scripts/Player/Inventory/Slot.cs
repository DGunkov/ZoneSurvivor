using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Vector2Int pos;
    internal bool full;

    private Grid_Cells grid_cells;

    private void Start()
    {
        grid_cells = GetComponentInParent<Grid_Cells>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag.gameObject;
        Item item_script = item.GetComponent<Item>();
        if(item_script != null)
        {
            if (!full)
            {
                if (item_script.size.x == 1 && item_script.size.y == 1)
                {
                    item_script.SetNewParent(transform);
                }
                else
                {
                    if (grid_cells != null && grid_cells.CheckPlace(pos, item_script.size, item_script))
                    {
                        item_script.SetNewParent(transform);
                    }
                }
            }
        }
        
    }
}

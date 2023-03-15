using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Cells : MonoBehaviour
{
    [SerializeField] List<Slot> cells;
    private Slot[,] cells_grid;
    void Start()
    {
        cells_grid = new Slot[5, 5];
        foreach(Slot slot in cells)
        {
            cells_grid[slot.pos.x, slot.pos.y] = slot;
        }
    }
   
    internal bool CheckPlace(Vector2Int pos, Vector2Int size, Item item)
    {
        if(cells_grid[pos.x + size.x - 1, pos.y] == null || pos.y - size.y + 1 < 0)
        {
            return false;
        }
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (cells_grid[pos.x + x, pos.y - y].full)
                {
                    return false;
                }
            }
        }        

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                cells_grid[pos.x + x, pos.y - y].full = true;
                item.occupied_slots.Add(cells_grid[pos.x + x, pos.y - y]);
            }
        }
        return true;
    }
    
}

using UnityEngine;
using UnityEngine.EventSystems;

public class Slot_For_Gun : MonoBehaviour, IDropHandler
{
    [SerializeField] private int min_size_for_gun;
    internal bool full;

    bool ItsWeapon(GameObject item)
    {
        return item.TryGetComponent<Weapon_Item>(out Weapon_Item gun_script) && gun_script.gun.size.x >= min_size_for_gun;
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject item = eventData.pointerDrag.gameObject;

        if (!full && item.TryGetComponent<Item>(out Item item_script) && ItsWeapon(item))
        {
            if (item.transform.rotation.z != 0)
            {
                item_script.Rotate();
            }
            item_script.rotate = false;
            item_script.SetNewParent(transform);
            full = true;
        }
    }
}

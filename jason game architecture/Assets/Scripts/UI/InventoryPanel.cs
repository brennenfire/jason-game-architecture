using System.Collections;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return null;
        Bind(Inventory.Instance);    
    }

    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>();
        for(int i = 0; i < panelSlots.Length; i++) 
        {
            panelSlots[i].Bind(inventory.GeneralSlots[i]);
        }
    }
}

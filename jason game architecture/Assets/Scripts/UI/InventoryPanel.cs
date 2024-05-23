using System.Collections;
using System.Linq;
using UnityEngine;

public class InventoryPanel : ToggleablePanel
{
    [SerializeField] InventoryPanelSlot overflowSlot;

    void Start()
    {
        Bind(Inventory.Instance);    
    }

    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>()
            .Where(t => t != overflowSlot).ToArray();
        for(int i = 0; i < panelSlots.Length; i++) 
        {
            panelSlots[i].Bind(inventory.GeneralSlots[i]);
        }
        overflowSlot.Bind(inventory.TopOverflowSlot);
    }
}

using Microsoft.Unity.VisualStudio.Editor;

public class EquipmentPanel : ToggleablePanel
{
    public void Bind(Inventory inventory)
    {
        var panelSlots = GetComponentsInChildren<InventoryPanelSlot>();
        foreach (var panelSlot in panelSlots)
        {
            panelSlot.Bind(inventory.GetEquipmentSlots(panelSlot.EquipmentSlotType));
        }
    }
}
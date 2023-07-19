using System;

[Serializable]
public class ItemSlot
{
    public Item Item;

    public bool isEmpty => Item == null;

    public void SetItem(Item item)
    {
        Item = item;       
    }
}
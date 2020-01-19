using UnityEngine;

public class Crafting : MonoBehaviour {
    [HideInInspector] public Item mouseHoldingItem;

    private Item[] craftingSlots = new Item[2];

    public static Crafting inst;

    private void Awake() {
        inst = this;
    }

    public void SetSlot(sbyte slot, Item item) {
        craftingSlots[slot] = item;
    }
}
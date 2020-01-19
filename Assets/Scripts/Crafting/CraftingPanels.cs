using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingPanels : MonoBehaviour, IDropHandler {
    public sbyte slotNumber;
    public bool canBePlacedIn;

    private GameObject itemDragerParent;

    private void Start() {
        itemDragerParent = GameObject.FindGameObjectWithTag("ItemDragerParent");
    }

    public void OnDrop(PointerEventData eventDate) {
        print("hello");
        if (Crafting.inst.mouseHoldingItem != null && canBePlacedIn) {
            GameObject itemObject = Crafting.inst.mouseHoldingItem.itemObject;
            RectTransform rectTransform = itemObject.GetComponent<RectTransform>();

            itemObject.transform.SetParent(transform);
            itemObject.transform.position = transform.position;

            Crafting.inst.SetSlot(slotNumber, Crafting.inst.mouseHoldingItem);
            Crafting.inst.mouseHoldingItem = null;
        }
    }
}
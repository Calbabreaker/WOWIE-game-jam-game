using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingPanels : MonoBehaviour, IDropHandler {
    public byte slotNumber;
    public bool canBePlacedIn;
    public bool isOutput;

    private GameObject itemDragerParent;

    private void Start() {
        itemDragerParent = GameObject.FindGameObjectWithTag("ItemDragerParent");
    }

    public void OnDrop(PointerEventData eventDate) {
        if (Crafting.inst.mouseHoldingItem != null && canBePlacedIn && Crafting.inst.GetInputSlot(slotNumber) == null) {
            GameObject itemObject = Crafting.inst.mouseHoldingItem.itemObject;
            print(itemObject);
            RectTransform rectTransform = itemObject.GetComponent<RectTransform>();

            itemObject.transform.SetParent(transform);
            itemObject.transform.position = transform.position;

            Crafting.inst.SetInputSlot(slotNumber, Crafting.inst.mouseHoldingItem);
            Crafting.inst.mouseHoldingItem = null;
            Crafting.inst.justTriedCraftingRecipe = false;
        }
    }
}
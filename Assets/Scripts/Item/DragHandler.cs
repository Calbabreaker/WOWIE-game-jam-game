using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    private RectTransform rectTransform;
    private GameObject itemDragerParent;
    private GameObject workSpace;
    private GameObject grid;
    private ItemDisplayer itemDisplayer;

    public bool canBeMoved = true;

    private void Start() {
        itemDisplayer = transform.GetChild(0).GetComponent<ItemDisplayer>();
        rectTransform = GetComponent<RectTransform>();
        itemDragerParent = GameObject.FindGameObjectWithTag("ItemDragerParent");
        workSpace = GameObject.FindGameObjectWithTag("WorkSpace");
        grid = GameObject.FindGameObjectWithTag("Grid");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (canBeMoved) {
            if (transform.parent.parent.name == "Crafting") {
                CraftingPanels craftingPanels = transform.parent.GetComponent<CraftingPanels>();
                if (craftingPanels.isOutput) {
                    if (Crafting.inst.GetOutputSlot(craftingPanels.slotNumber) != null) {
                        if (Crafting.inst.GetOutputSlot(craftingPanels.slotNumber).itemObject.GetComponent<DragHandler>().canBeMoved) {
                            Inventory.inst.AddNewDiscoveredItem(Crafting.inst.GetOutputSlot(craftingPanels.slotNumber));
                            Crafting.inst.SetOutputSlot(craftingPanels.slotNumber, null);
                        }
                    }
                } else {
                    if (Crafting.inst.GetInputSlot(craftingPanels.slotNumber) != null) {
                        Crafting.inst.SetInputSlot(craftingPanels.slotNumber, null);
                        Crafting.inst.justTriedCraftingRecipe = false;

                        if (Crafting.inst.GetOutputSlot(0) != null) {
                            GameObject itemObjectOutput1 = Crafting.inst.GetOutputSlot(0).itemObject;
                            Destroy(itemObjectOutput1);
                            Crafting.inst.SetOutputSlot(0, null);
                            if (Crafting.inst.GetOutputSlot(1) != null) {
                                GameObject itemObjectOutput2 = Crafting.inst.GetOutputSlot(1).itemObject;
                                Destroy(itemObjectOutput2);
                                Crafting.inst.SetOutputSlot(1, null);
                            }
                        }
                    }
                }
            }

            transform.SetParent(itemDragerParent.transform);
            Crafting.inst.mouseHoldingItem = itemDisplayer.item;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        if (canBeMoved) rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (Crafting.inst.mouseHoldingItem != null) {
            StartCoroutine(WaitAndContinueEndDrag());
        }
    }

    public IEnumerator WaitAndContinueEndDrag() {
        yield return null;
        transform.SetParent(grid.transform);
        Crafting.inst.mouseHoldingItem = null;
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
    private RectTransform rectTransform;
    private GameObject itemDragerParent;
    private GameObject workSpace;
    private GameObject grid;
    private ItemDisplayer itemDisplayer;
    private RawImage rawImage;
    private Animator animator;

    public bool canBeMoved = true;

    private void Start() {
        if (transform.childCount == 1) {
            ActualStart();
        }
    }

    public void ActualStart() {
        animator = transform.GetChild(0).GetComponent<Animator>();
        itemDisplayer = transform.GetChild(0).GetComponent<ItemDisplayer>();
        rectTransform = GetComponent<RectTransform>();
        itemDragerParent = GameObject.FindGameObjectWithTag("ItemDragerParent");
        workSpace = GameObject.FindGameObjectWithTag("WorkSpace");
        grid = GameObject.FindGameObjectWithTag("Grid");
        rawImage = transform.GetChild(0).GetComponent<RawImage>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (canBeMoved) {
            if (transform.parent.parent.name == "Crafting") {
                CraftingPanels craftingPanels = transform.parent.GetComponent<CraftingPanels>();
                if (craftingPanels.isOutput) {
                    if (Crafting.inst.GetOutputSlot() != null) {
                        if (Crafting.inst.GetOutputSlot().itemObject.GetComponent<DragHandler>().canBeMoved) {
                            animator.SetTrigger("Stop");
                            Inventory.inst.AddNewItemInInventory(Crafting.inst.GetOutputSlot());
                            Crafting.inst.SetOutputSlot(null);
                        }
                    }
                } else {
                    if (Crafting.inst.GetInputSlot(craftingPanels.slotNumber) != null) {
                        Crafting.inst.SetInputSlot(craftingPanels.slotNumber, null);
                        Crafting.inst.justTriedCraftingRecipe = false;
                        Crafting.inst.itemInQueuRecipe = null;

                        if (Crafting.inst.GetOutputSlot() != null) {
                            GameObject itemObjectOutput1 = Crafting.inst.GetOutputSlot().itemObject;
                            Destroy(itemObjectOutput1);
                            Crafting.inst.SetOutputSlot(null);
                            if (Crafting.inst.GetOutputSlot() != null) {
                                GameObject itemObjectOutput2 = Crafting.inst.GetOutputSlot().itemObject;
                                Destroy(itemObjectOutput2);
                                Crafting.inst.SetOutputSlot(null);
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
        if (Crafting.inst.itemInQueuRecipe != null) {
            Crafting.inst.NewItemFromRecipe(Crafting.inst.itemInQueuRecipe, 1);
            Crafting.inst.itemInQueuRecipe = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //print("mousey");
        rawImage.color = new Color(0.8f, 0.8f, 0.8f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        rawImage.color = new Color(1, 1, 1);
    }
}
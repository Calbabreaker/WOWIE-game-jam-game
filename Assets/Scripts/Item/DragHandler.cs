using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {
    private RectTransform rectTransform;
    private GameObject itemDragerParent;
    private GameObject workSpace;
    private GameObject grid;
    private ItemDisplayer itemDisplayer;

    private void Start() {
        itemDisplayer = GetComponent<ItemDisplayer>();
        rectTransform = GetComponent<RectTransform>();
        itemDragerParent = GameObject.FindGameObjectWithTag("ItemDragerParent");
        print(itemDragerParent);
        workSpace = GameObject.FindGameObjectWithTag("WorkSpace");
        grid = GameObject.FindGameObjectWithTag("Grid");
    }

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(itemDragerParent.transform);
        Crafting.inst.mouseHoldingItem = itemDisplayer.item;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.position = Input.mousePosition;
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
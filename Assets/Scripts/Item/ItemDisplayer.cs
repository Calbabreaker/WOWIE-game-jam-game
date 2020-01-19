using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplayer : MonoBehaviour {
    [HideInInspector] public TextMeshProUGUI nameTMP;
    [HideInInspector] public RawImage imageRenderer;
    private DragHandler dragHandler;

    public Item item;

    private void Start() {
        item.itemObject = transform.parent.gameObject;
        nameTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        imageRenderer = GetComponent<RawImage>();
        dragHandler = transform.parent.GetComponent<DragHandler>();

        nameTMP.text = item.name;
        imageRenderer.texture = item.sprite;
    }

    public void DisableItem() {
        Start();
        dragHandler.canBeMoved = false;
        imageRenderer.color = new Color(1, 1, 1, 0.5f);
        nameTMP.color = new Color(1, 1, 1, 0.5f);
    }
}
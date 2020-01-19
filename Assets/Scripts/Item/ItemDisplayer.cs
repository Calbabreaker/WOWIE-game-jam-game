using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplayer : MonoBehaviour {
    [HideInInspector] public TextMeshProUGUI nameTMP;
    [HideInInspector] public RawImage imageRenderer;

    public Item item;

    private void Start() {
        item.itemObject = gameObject;
        nameTMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        imageRenderer = GetComponent<RawImage>();

        nameTMP.text = item.name;
        imageRenderer.texture = item.sprite;
    }
}
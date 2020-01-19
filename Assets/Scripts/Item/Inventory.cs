using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Inventory : MonoBehaviour {
    [HideInInspector] public List<Item> discoveredItems = new List<Item>(0);
    public int totalAmountOfItems;

    private TextMeshProUGUI amountOfItemsText;

    public static Inventory inst;

    private void Awake() {
        inst = this;
    }

    private void Start() {
        amountOfItemsText = transform.Find("AmountOfItemsText").GetComponent<TextMeshProUGUI>();
        amountOfItemsText.text = $"{discoveredItems.Count}/{totalAmountOfItems}";
    }

    public void AddNewDiscoveredItem(Item item) {
        discoveredItems.Add(item);
        amountOfItemsText.text = $"{discoveredItems.Count}/{totalAmountOfItems}";
    }
}
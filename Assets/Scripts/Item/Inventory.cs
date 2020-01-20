using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Inventory : MonoBehaviour {
    public List<Item> discoveredItems = new List<Item>(0);
    public List<Item> itemsInInventory = new List<Item>(0);
    public int totalAmountOfItems;

    private TextMeshProUGUI amountOfItemsText;

    public static Inventory inst;

    private void Awake() {
        inst = this;
    }

    private void Start() {
        amountOfItemsText = transform.Find("AmountOfItemsText").GetComponent<TextMeshProUGUI>();
        amountOfItemsText.text = $"{itemsInInventory.Count}/{totalAmountOfItems}";
    }

    public void AddNewItemInInventory(Item item) {
        itemsInInventory.Add(item);
        amountOfItemsText.text = $"{itemsInInventory.Count}/{totalAmountOfItems}";
    }

    public void ShowItemInDiscoveredScreen(Item item) {

    }
}
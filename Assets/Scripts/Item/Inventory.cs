using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Inventory : MonoBehaviour {
    public List<Item> discoveredItems = new List<Item>(0);
    public List<Item> itemsInInventory = new List<Item>(0);
    public int totalAmountOfItems;
    public Recipe unknownRecipe;

    private TextMeshProUGUI amountOfItemsText;

    public static Inventory inst;

    private void Awake() {
        inst = this;
    }

    private void Start() {
        amountOfItemsText = transform.Find("AmountOfItemsText").GetComponent<TextMeshProUGUI>();
        amountOfItemsText.text = $"{itemsInInventory.Count}/{totalAmountOfItems}";
    }

    private void Update() {
        if (itemsInInventory.Count == 10 && Crafting.inst.itemInQueuRecipe == null && unknownRecipe != null) {
            Crafting.inst.itemInQueuRecipe = unknownRecipe;
            unknownRecipe = null;
        }
    }

    public void AddNewItemInInventory(Item item) {
        itemsInInventory.Add(item);
        amountOfItemsText.text = $"{itemsInInventory.Count}/{totalAmountOfItems}";
    }

    public void AddNewDiscoveredItem(Item item) {
        discoveredItems.Add(item);
    }
}
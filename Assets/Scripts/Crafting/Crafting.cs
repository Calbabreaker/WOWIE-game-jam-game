using UnityEngine;

public class Crafting : MonoBehaviour {
    [HideInInspector] public Item mouseHoldingItem;
    public GameObject itemObjectPrefab;
    public GameObject[] outputSlotObjects = new GameObject[2];

    private Item[] inputSlots = new Item[2];
    private Item[] outputSlots = new Item[2];

    [HideInInspector] public bool triedCraftingRecipe = false;

    public static Crafting inst;

    private void Awake() {
        inst = this;
    }

    private void Start() {
        outputSlotObjects[0] = transform.Find("CraftingOutput1").gameObject;
    }

    public void Update() {
        if (inputSlots[0] != null && inputSlots[1] != null && !triedCraftingRecipe) {
            foreach (Recipe recipe in inputSlots[0].recipes) {
                if (recipe.inputItems[0].name == inputSlots[1].name || recipe.inputItems[1].name == inputSlots[1].name) {
                    NewItem(recipe, 0);
                    
                    if (recipe.resultItems.Count == 2) {
                        NewItem(recipe, 1);
                    }

                    triedCraftingRecipe = true;
                    return;
                }
            }

            triedCraftingRecipe = true;
        }
    }

    private Item NewItem(Recipe recipe, byte slotNumber) {
        GameObject itemObject = Instantiate(itemObjectPrefab, outputSlotObjects[slotNumber].transform.position, Quaternion.identity, outputSlotObjects[0].transform);
        ItemDisplayer itemDisplayer = itemObject.GetComponent<ItemDisplayer>();
        itemDisplayer.item = recipe.resultItems[slotNumber];
        itemDisplayer.item.itemObject = itemObject;
        SetOutputSlot(slotNumber, itemDisplayer.item);
        foreach (Item item in Inventory.inst.discoveredItems) {
            if (item.name == itemDisplayer.item.name) {
                itemDisplayer.DisableItem();
            }
        }

        return itemDisplayer.item;
    }

    public void SetInputSlot(byte slot, Item item) {
        inputSlots[slot] = item;
    }

    public void SetOutputSlot(byte slot, Item item) {
        outputSlots[slot] = item;
    }

    public Item GetInputSlot(byte slot) {
        return inputSlots[slot];
    }

    public Item GetOutputSlot(byte slot) {
        return outputSlots[slot];
    }
}
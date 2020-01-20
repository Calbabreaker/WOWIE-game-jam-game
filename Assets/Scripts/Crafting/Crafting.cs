using UnityEngine;

public class Crafting : MonoBehaviour {
    [HideInInspector] public Item mouseHoldingItem;
    public GameObject itemObjectPrefab;
    public GameObject outputSlotObject;
    public Recipe itemInQueuRecipe;

    private Item[] inputSlots = new Item[2];
    private Item outputSlot;

    [HideInInspector] public bool justTriedCraftingRecipe = false;

    public static Crafting inst;

    private void Awake() {
        inst = this;
    }

    private void Start() {
        outputSlotObject = transform.Find("CraftingOutput").gameObject;
    }

    public void Update() {
        if (inputSlots[0] != null && inputSlots[1] != null && !justTriedCraftingRecipe && outputSlot == null && outputSlot == null) {
            foreach (Recipe recipe in inputSlots[0].recipes) {
                if (recipe.inputItems[0].name == inputSlots[1].name || recipe.inputItems[1].name == inputSlots[1].name) {
                    if (recipe.resultItems.Count == 2) {
                        itemInQueuRecipe = recipe;
                    }

                    NewItemFromRecipe(recipe, 0);

                    justTriedCraftingRecipe = true;
                    return;
                }
            }

            Animator itemAnimator1 = inputSlots[0].itemObject.transform.GetChild(0).GetComponent<Animator>();
            Animator itemAnimator2 = inputSlots[1].itemObject.transform.GetChild(0).GetComponent<Animator>();
            itemAnimator1.SetTrigger("RecipeNotFound");
            itemAnimator2.SetTrigger("RecipeNotFound");
            justTriedCraftingRecipe = true;
        }
    }

    public Item NewItemFromRecipe(Recipe recipe, byte slotNumber) {
        GameObject itemObject = Instantiate(itemObjectPrefab, outputSlotObject.transform.position, Quaternion.identity, outputSlotObject.transform);
        ItemDisplayer itemDisplayer = itemObject.transform.GetChild(0).GetComponent<ItemDisplayer>();
        Animator animator = itemDisplayer.GetComponent<Animator>();
        itemDisplayer.item = recipe.resultItems[slotNumber];
        SetOutputSlot(itemDisplayer.item);
        itemDisplayer.ActualStart();
        itemObject.GetComponent<DragHandler>().ActualStart();

        bool isItemInInventory = false;
        foreach (Item item in Inventory.inst.itemsInInventory) {
            if (item.name == itemDisplayer.item.name) {
                isItemInInventory = true;
                itemInQueuRecipe = null;
                itemDisplayer.DisableItem();
                break;
            }
        }

        itemDisplayer.SetItemObject();

        if (!isItemInInventory) {
            animator.SetTrigger("NewItem");
        }

        bool isItemInDiscoveredItems = false;
        foreach (Item item in Inventory.inst.discoveredItems) {
            if (item.name == itemDisplayer.item.name) {
                isItemInDiscoveredItems = true;
                break;
            }
        }

        if (!isItemInDiscoveredItems) {
            DiscoveredScreen.inst.ShowItemInDiscoveredScreen(itemDisplayer);
        }

        return itemDisplayer.item;
    }

    public void SetInputSlot(byte slot, Item item) {
        inputSlots[slot] = item;
    }

    public void SetOutputSlot(Item item) {
        outputSlot = item;
    }

    public Item GetInputSlot(byte slot) {
        return inputSlots[slot];
    }

    public Item GetOutputSlot() {
        return outputSlot;
    }
}
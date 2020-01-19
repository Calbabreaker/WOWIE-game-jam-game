using UnityEngine;

public class Crafting : MonoBehaviour {
    [HideInInspector] public Item mouseHoldingItem;
    public GameObject itemObjectPrefab;
    private GameObject[] outputSlotObjects = new GameObject[2];

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
                    GameObject itemObject = Instantiate(itemObjectPrefab, outputSlotObjects[0].transform.position, Quaternion.identity, outputSlotObjects[0].transform);
                    ItemDisplayer itemDisplayer = itemObject.GetComponent<ItemDisplayer>();
                    itemDisplayer.item = recipe.resultItems[0];
                    itemDisplayer.item.itemObject = itemObject;
                    
                    if (recipe.resultItems.Count == 2) {
                        itemObject = Instantiate(itemObjectPrefab, outputSlotObjects[1].transform.position, Quaternion.identity, outputSlotObjects[1].transform);
                    }

                    triedCraftingRecipe = true;
                    return;
                }
            }

            triedCraftingRecipe = true;
        }
    }

    public void SetInputSlot(sbyte slot, Item item) {
        inputSlots[slot] = item;
    }

    public Item GetInputSlot(sbyte slot) {
        return inputSlots[slot];
    }

    public Item GetOuputSlot(sbyte slot) {
        return outputSlots[slot];
    }
}
using UnityEngine;
using System.Collections;
using TMPro;

public class DiscoveredScreen : MonoBehaviour {
    private GameObject itemHolder;
    private GameObject discoveredScreen;
    private TextMeshProUGUI descriptionBox;
    private GameObject button;

    private ItemDisplayer currentItemDisplayer;

    public static DiscoveredScreen inst;

    private void Awake() {
        inst = this;
    }

    private void Start() {
        itemHolder = transform.GetChild(0).transform.Find("ItemHolder").gameObject;
        discoveredScreen = transform.Find("DiscoveredScreen").gameObject;
        descriptionBox = transform.GetChild(0).Find("Description").GetComponent<TextMeshProUGUI>();
        button = transform.GetChild(0).Find("Ok Button").gameObject;
    }

    public void ShowItemInDiscoveredScreen(ItemDisplayer itemDisplayer) {
        discoveredScreen.SetActive(true);
        itemDisplayer.transform.SetParent(itemHolder.transform);
        itemDisplayer.transform.position = itemHolder.transform.position;
        descriptionBox.text = itemDisplayer.item.discoverDescription;
        currentItemDisplayer = itemDisplayer;
        Inventory.inst.AddNewDiscoveredItem(itemDisplayer.item);
    }

    public void HideDiscoveredScreenAndPutBackItem() {
        Animator descriptionBoxAnimator = descriptionBox.GetComponent<Animator>();
        Animator buttonAnimator = button.GetComponent<Animator>();
        descriptionBoxAnimator.SetTrigger("Hide");
        buttonAnimator.SetTrigger("Hide");
        StartCoroutine(WaitAndDoIt());
    }

    private IEnumerator WaitAndDoIt() {
        yield return new WaitForSeconds(0.2f);
        currentItemDisplayer.transform.SetParent(currentItemDisplayer.item.itemObject.transform);
        currentItemDisplayer.transform.position = currentItemDisplayer.item.itemObject.transform.position;
        currentItemDisplayer = null;
        discoveredScreen.SetActive(false);
    }
}
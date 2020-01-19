using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "GameAssets/Create New Item", order = 0)]
public class Item : ScriptableObject {
    public new string name;
    public Texture sprite;

    public string discoverTitle;
    [TextArea] public string discoverDescription;

    [HideInInspector]
    public GameObject itemObject;
}
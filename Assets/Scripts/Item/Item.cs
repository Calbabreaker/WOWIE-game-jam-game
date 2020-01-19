using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Item", menuName = "GameAssets/Create New Item", order = 0)]
public class Item : ScriptableObject {
    public new string name;
    public Texture sprite;

    [TextArea] public string discoverDescription;
    public List<Recipe> recipes = new List<Recipe>(1);

    [HideInInspector]
    public GameObject itemObject;
}
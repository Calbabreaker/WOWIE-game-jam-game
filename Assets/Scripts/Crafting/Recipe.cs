using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "GameAssets/Create New Recipe", order = 0)]
public class Recipe : ScriptableObject {
    public List<Item> inputItems = new List<Item>(2);
    public List<Item> resultItems = new List<Item>(1);
}
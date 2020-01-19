using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Recipe", menuName = "GameAssets/Create New Recipe", order = 0)]
public class Recipes : ScriptableObject {
    public List<Item> inputItems = new List<Item>();
    public List<Item> resultItems = new List<Item>();
}
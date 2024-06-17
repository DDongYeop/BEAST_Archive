using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class IngredientDrop
{
    [FormerlySerializedAs("ingredient")] public IngredientType Ingredient;
    public int DropPercent;

    //x==����, y==percent
    public List<Vector2Int> CntAndPercnet;
}

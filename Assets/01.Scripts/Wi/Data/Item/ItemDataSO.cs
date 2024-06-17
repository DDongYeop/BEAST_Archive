using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameData/Item")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    [TextArea]
    public string description;
}

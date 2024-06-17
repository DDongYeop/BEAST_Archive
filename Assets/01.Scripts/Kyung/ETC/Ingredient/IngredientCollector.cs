using System.Collections.Generic;
using UnityEngine;

public class IngredientCollector : MonoBehaviour
{
    public static IngredientCollector Instance = null;

    private Dictionary<IngredientType, int> _ingredientCollectorDic = new Dictionary<IngredientType, int>();

    [Header("Item")] 
    public List<Sprite> ItemSprites;
    
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Multiple ItemCollector");
        Instance = this;
    }

    public void AddItem(IngredientType type, int cnt = 1)
    {
        if (_ingredientCollectorDic.ContainsKey(type))
            _ingredientCollectorDic[type] += cnt;
        else
            _ingredientCollectorDic.TryAdd(type, cnt);
    }

    public Dictionary<IngredientType, int> GetItems()
    {
        return _ingredientCollectorDic;
    }
}

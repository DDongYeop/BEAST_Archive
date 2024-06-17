using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/Enemy/ItemDrop", fileName = "ItemDropSO")]
public class IngredientDropSO : ScriptableObject
{
    [SerializeField] private List<IngredientDrop> _ingredientDrop;

    public void ItemDrop(Transform trm)
    {
        foreach (var ingredientDrop in _ingredientDrop)
        {
            int rand = Random.Range(1, 101);
            if (rand > ingredientDrop.DropPercent)
                continue;
            
            int cnt = Random.Range(1, 101);
            int addValue = 0;
            foreach (var cntAndDrop in ingredientDrop.CntAndPercnet)
            {
                addValue += cntAndDrop.y;
                if (cnt <= addValue)
                {
                    cnt = cntAndDrop.x;
                    break;
                }
            }
            
            for (int i = 0; i < cnt; ++i)
            {
                Transform item = PoolManager.Instance.Pop("Item").transform;
                item.position = trm.position + new Vector3(0, .5f, 0);
                item.GetComponent<SpriteRenderer>().sprite = IngredientCollector.Instance.ItemSprites[(int)ingredientDrop.Ingredient];
                item.GetComponent<Ingredient>().Type = ingredientDrop.Ingredient;
            }
        }
    }
}

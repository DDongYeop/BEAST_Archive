using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameData/StageData")]
public class StageDataSO : ScriptableObject
{
    public MapDataSO mapData;
    public BossDataSO bossData;
    public EnemyGroupSO enemyGroup;
    public List<ItemDataSO> dropItems;
}

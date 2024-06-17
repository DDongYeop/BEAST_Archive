using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameData/EnemyGroup")]
public class EnemyGroupSO : ScriptableObject
{
    public List<EnemyBrain> enemies;
}

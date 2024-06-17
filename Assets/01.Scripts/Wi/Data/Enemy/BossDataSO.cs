using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameData/Boss")]
public class BossDataSO : ScriptableObject
{
    public string bossName;
    public Sprite bossImage;
    public EnemyBrain bossPrefab;
}

using UnityEngine;

// UI와 상호작용을 위한 SO
[CreateAssetMenu(menuName = "SO/ThronwWeapon/TresureInfo")]
public class TreasureInfo : ScriptableObject
{
    // 보물 ID
    [SerializeField] private string treasureId;
    public string TreasureId => treasureId;

    // 보물 이름 (For UI)
    [SerializeField] private string treasureIdName;
    public string TreasureIdName => treasureIdName;

    // 보물 이미지
    [SerializeField] private Sprite treasureSprite;
    public Sprite TreasureSprite => treasureSprite;

    [SerializeField] private Vector2 spritePivotPosition = Vector2.zero;
    public Vector2 SpritePivotPosition => spritePivotPosition;
}

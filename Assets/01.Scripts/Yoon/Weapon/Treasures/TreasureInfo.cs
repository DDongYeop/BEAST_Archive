using UnityEngine;

// UI�� ��ȣ�ۿ��� ���� SO
[CreateAssetMenu(menuName = "SO/ThronwWeapon/TresureInfo")]
public class TreasureInfo : ScriptableObject
{
    // ���� ID
    [SerializeField] private string treasureId;
    public string TreasureId => treasureId;

    // ���� �̸� (For UI)
    [SerializeField] private string treasureIdName;
    public string TreasureIdName => treasureIdName;

    // ���� �̹���
    [SerializeField] private Sprite treasureSprite;
    public Sprite TreasureSprite => treasureSprite;

    [SerializeField] private Vector2 spritePivotPosition = Vector2.zero;
    public Vector2 SpritePivotPosition => spritePivotPosition;
}

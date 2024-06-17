using UnityEngine;

// UI�� ��ȣ�ۿ��� ���� SO
[CreateAssetMenu(menuName = "SO/ThronwWeapon/WeaponInfo")]
public class ThrownWeaponInfo : ScriptableObject
{
    [SerializeField] private string weaponId;
    public string WeaponId => weaponId;

    // ���� �̸� (For UI)
    [SerializeField] private string weaponName;
    public string WeaponName => weaponName;

    // ���� �̹���
    [SerializeField] private Sprite weaponSprite;
    public Sprite WeaponSprite => weaponSprite;

    [SerializeField] private Vector2 spritePivotPosition = Vector2.zero;
    public Vector2 SpritePivotPosition => spritePivotPosition;
}

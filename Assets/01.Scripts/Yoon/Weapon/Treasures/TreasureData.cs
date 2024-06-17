using UnityEngine;

[CreateAssetMenu(menuName = "SO/ThronwWeapon/TresureData")]
public class TreasureData : ScriptableObject
{
    [SerializeField] private string treasureId;
    public string TreasureId => treasureId;

    [SerializeField] private TreasureType treasureType; // Skill Component 생성하기 위한 Reflection에 사용
    public TreasureType TreasureType => treasureType;

    [SerializeField] private int countForRecharge; // 보물 효과 충전을 위한 발사 횟수
    public int CountForRecharge => countForRecharge;

    [SerializeField] private int damage = 0; // 공격력, 공격력 업그레이드 정도를 나타냄
    public int Damage => damage;

    [SerializeField] private float duration = 0f; // 발동 시간
    public float Duration => duration;

}

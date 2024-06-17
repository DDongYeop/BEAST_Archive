using UnityEngine;

[CreateAssetMenu(menuName = "SO/ThronwWeapon/TresureData")]
public class TreasureData : ScriptableObject
{
    [SerializeField] private string treasureId;
    public string TreasureId => treasureId;

    [SerializeField] private TreasureType treasureType; // Skill Component �����ϱ� ���� Reflection�� ���
    public TreasureType TreasureType => treasureType;

    [SerializeField] private int countForRecharge; // ���� ȿ�� ������ ���� �߻� Ƚ��
    public int CountForRecharge => countForRecharge;

    [SerializeField] private int damage = 0; // ���ݷ�, ���ݷ� ���׷��̵� ������ ��Ÿ��
    public int Damage => damage;

    [SerializeField] private float duration = 0f; // �ߵ� �ð�
    public float Duration => duration;

}

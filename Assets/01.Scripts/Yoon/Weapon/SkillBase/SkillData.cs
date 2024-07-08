using UnityEngine;

[CreateAssetMenu(menuName = "SO/ThronwWeapon/SkillData")]
public class SkillData : ScriptableObject
{
    public string SkillId => skillType.ToString();

    [SerializeField] private SkillType skillType; // Skill Component �����ϱ� ���� Reflection�� ���
    public SkillType SkillType => skillType;

    [SerializeField] private int countForRecharge; // ��ų ī��Ʈ ������ ���� �߻� Ƚ��
    public int CountForRecharge => countForRecharge;

    [SerializeField] private int damage = 0; // ���ݷ�, ���ݷ� ���׷��̵� ������ ��Ÿ��
    public int Damage => damage;

    [SerializeField] private float duration = 0f; // �ߵ� �ð�
    public float Duration => duration;

    public string CurrentWeaponId { get; set; }

    [SerializeField] private bool runImmediately = false; // ��� �ߵ�����
    public bool RunImmediately => runImmediately;

}

using UnityEngine;

public class DamageUpComponent : TreasureSkill
{
    public override void UseSkill(Transform targetTransform, int damage, float duration)
    {
        // �߰� ������
        bool isCheck = targetTransform.TryGetComponent(out AgentHealth enemyHealth);
        if (isCheck)
        {
            if (enemyHealth.CurrentHp > 0)
            {
                enemyHealth.OnDamage(damage, Vector3.zero);
            }
        }
    }
}

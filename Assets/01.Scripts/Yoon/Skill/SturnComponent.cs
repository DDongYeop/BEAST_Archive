using UnityEngine;

public class SturnComponent : TreasureSkill
{
    public override void UseSkill(Transform targetTransform, int damage, float duration)
    {
        SturnTarget(targetTransform, duration);
    }

    public void SturnTarget(Transform targetTransform, float sturnTime)
    {
        if (targetTransform.root.TryGetComponent(out AgentHealth enemyHealth))
        {
            enemyHealth.Stun(sturnTime);
        }
    }

}

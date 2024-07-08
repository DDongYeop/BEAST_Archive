using UnityEngine;

public class EnemyHealth : AgentHealth
{
    public override void OnDamage(int damage, Vector3 hitPos)
    {
        base.OnDamage(damage, hitPos);
        
        if (_brain.CurrentNode && _brain.CurrentNode.IsAttackStop)
            _brain.AgentAnimator.SetAnimEnd();
    }
}

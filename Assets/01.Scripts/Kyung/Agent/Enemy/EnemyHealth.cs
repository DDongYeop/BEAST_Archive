using UnityEngine;

public class EnemyHealth : AgentHealth
{
    public override void OnDamage(int damage, Vector3 hitPos)
    {
        base.OnDamage(damage, hitPos);
        
        if (_brain.CurrentNode && _brain.CurrentNode.IsAttackStop)
            _brain.AgentAnimator.SetAnimEnd();
    }

    [ContextMenu("Damage")]
    private void Damage()
    {
        OnDamage(1000000000, Vector3.zero);
    }
}

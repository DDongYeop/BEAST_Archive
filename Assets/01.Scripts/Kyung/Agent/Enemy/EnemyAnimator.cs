public class EnemyAnimator : AgentAnimator
{
    public override void SetAnimEnd()
    {
        base.SetAnimEnd();
        
        if (_brain.CurrentNode)
            _brain.CurrentNode.OnStop();
        _brain.CurrentNodeValue = -1;
    }
}

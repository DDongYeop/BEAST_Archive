namespace Behaviour
{
    public class AnimationNode : ActionNode
    {
        protected override State OnUpdate()
        {
            return State.RUNNING;
        }
    }
}

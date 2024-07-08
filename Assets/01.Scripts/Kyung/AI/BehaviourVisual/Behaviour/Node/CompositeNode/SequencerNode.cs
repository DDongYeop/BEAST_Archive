namespace Behaviour
{
    public class SequencerNode : CompositeNode
    {
        private int _current = 0;

        public override void OnStart()
        {
            _current = 0;
        }

        public override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            bool _isSuccess = true;
            
            foreach (var child in Children)
            {
                switch (child.Update())
                {
                    case State.RUNNING:
                        return State.RUNNING;
                    case State.FAILURE:
                        _isSuccess = false;
                        break;
                    case State.SUCCESS:
                        break;
                }
            }

            return _isSuccess ? State.SUCCESS : State.FAILURE;
        }
    }
}

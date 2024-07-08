using UnityEngine;

namespace Behaviour
{
    public class MoveNode : ActionNode
    {
        [Header("Move Node")] 
        [SerializeField] private float _speed;
        [SerializeField] private bool _addDash;
        [SerializeField] private Vector3 _addPos;
        private ParticleLoop _particle;
        
        public override void OnStart()
        {
            base.OnStart();
            if (PoolManager.Instance != null && _addDash)
            {
                if (_particle)
                    PoolManager.Instance.Push(_particle);
                
                _particle = PoolManager.Instance.Pop("SpeedLineEffect") as ParticleLoop;
                _particle.transform.SetParent(Brain.transform);
                _particle.transform.localPosition = _addPos;
            }
        }

        public override void OnStop()
        {
            base.OnStop();
            
            if (_addDash && _particle)
                PoolManager.Instance.Push(_particle);
        }
        
        protected override State OnUpdate()
        {
            Brain.SetVelocityX(_speed * -1);
            return State.RUNNING;
        }
    }
}

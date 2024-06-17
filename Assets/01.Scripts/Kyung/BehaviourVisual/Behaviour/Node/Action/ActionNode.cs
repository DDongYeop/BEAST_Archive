using UnityEngine;

namespace Behaviour
{
    public abstract class ActionNode : Node
    {
        [Header("Condition")] 
        [SerializeField] private ConditionType _condition;
        [SerializeField] private float _benchmark;
        [SerializeField] private bool _isDown = false;
        
        [Header("CoolTime")] 
        [SerializeField] protected float _coolTime;
        protected float LastTime = 0;
        
        [Header("Animation")]
        [SerializeField] protected string _animationKey;

        [Header("Other")] 
        [SerializeField] protected bool _isShake;
        [SerializeField] protected float _shakePower;
        [SerializeField] protected float _shakeTime;
        public bool IsAttackStop;

        private EnemyFeedback _feedback;

        public override void Init(EnemyBrain brain, Blackboard blackboard)
        {
            base.Init(brain, blackboard);
            _feedback = Brain.GetComponent<EnemyFeedback>();
        }

        public bool IsCanPlay()
        {
            return IsCoolTime() && IsCondition();
        }

        public override Node Clone()
        {
            LastTime = Time.time;
            return base.Clone();
        }

        public override void OnStart()
        {
            LastTime = Time.time;
            Brain.AgentAnimator.OnOtherBool(_animationKey, true);
            
            if (_isShake)
                CameraManager.Instance.CameraShake(_shakePower, _shakeTime);
        }

        public override void OnStop()
        {
            Brain.AgentAnimator.OnOtherBool(_animationKey, false);
            
            if (_isShake)
                CameraManager.Instance.StopShake(); 
        }

        private bool IsCoolTime()
        {
            return (LastTime + _coolTime) < Time.time;
        }

        private bool IsCondition()
        {
            bool value = false;
            
            switch (_condition)
            {
                case ConditionType.NONE:
                    value = true;
                    break;
                case ConditionType.HP:
                    value = Condision(Brain.AgentHealth.CurrentHp);
                    break;
                case ConditionType.STICK:
                    value = Condision(Brain.WeaponStick.StickCount());
                    break;
                case ConditionType.DISTANCE:
                    value = Condision(Brain.transform.position.x - GameManager.Instance.PlayerTrm.position.x);
                    break; 
            }

            return _isDown ? !value : value;
        }

        private bool Condision(float value)
        {
            return _benchmark <= value;
        }
    }
}

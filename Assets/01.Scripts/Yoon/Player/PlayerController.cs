using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("�浹üũ")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject groundEffect;

    // Components
    public Animator Animator { get; private set; }

    // PlayerComponents
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    #region MainLogic

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        PlayerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();

        Transform visualTransform = transform.Find("Visual");
        Animator = visualTransform.GetComponent<Animator>();

        // StateMachine Setting
        StateMachine = new PlayerStateMachine();
        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            // Type ������ State�� ������ �Ű������� animationBoolName���ε� ����Ѵ�.
            string typeName = stateEnum.ToString();

            // PlayerState�� ��ӹް� �ִ� Ŭ�������� �����ͼ� StateMachine�� ��Ͻ��� �ش�.
            try
            {
                Type type = Type.GetType($"Player{typeName}State");
                PlayerState stateInstance = Activator.CreateInstance(type, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, stateInstance); 
            }
            catch (Exception e)
            {
                Debug.LogError($"{typeName} Ŭ���� �ν��Ͻ��� ������ �� �����ϴ�. {e.Message}");
            }
        }
    }

    private void OnEnable()
    {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);

        PlayerInput.AimingkEvent += HandleAimingEvent;
        playerHealth.OnPlayerDieEvent += DieEvent;
    }
    
    private void OnDisable()
    {
        PlayerInput.AimingkEvent -= HandleAimingEvent;
        playerHealth.OnPlayerDieEvent -= DieEvent;
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }

    private void DieEvent()
    {
        StateMachine.ChangeState(PlayerStateEnum.Die);
    }

    #endregion

    #region InputEvent

    private string onlyTypeWeapon = "Bow";
    public void HandleAimingEvent()
    {
        if (playerHealth.IsDead) return;

        if (PlayerAttack.EquipWeaponId == onlyTypeWeapon)
        {
            StateMachine.ChangeState(PlayerStateEnum.BowAming);
        }
        else
        {
            StateMachine.ChangeState(PlayerStateEnum.Aiming);
        }
    }

    #endregion

    #region Movement

        public void SetVelocity(float x)
        {
            playerMovement.SetVelocity(x);
        }

        public void ControllGroundEffect(bool isOn)
        {
            groundEffect.SetActive(isOn);
        }
        
        // ��� ����
        // public void StopImmediately(bool withYAxis = true)
        // {
        //     playerMovement.StopImmediately(withYAxis);
        // }

        #endregion

    #region ETC

    // ���� ��Ҵ���
    public bool IsGroundDetected
    {
        get
        {
            bool isHitGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
            return isHitGround;
        }
    }

    // Animation Event
    public void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationEndTrigger();
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawLine(throwTransform.position, throwTransform.position * (throwInfo.direction * 5f));
    // }

    #endregion

}

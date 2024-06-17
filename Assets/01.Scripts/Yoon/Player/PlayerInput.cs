using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool isEditor = true;

    // event
    public Action AimingkEvent;

    // Movement
    private float xInput;
    public float XInput => xInput;
    public bool IsMoveInputIn
    {
        get
        {
            return Mathf.Abs(xInput) > 0.05f;
        }
    }

    // Attack
    [SerializeField] private float touchCoolTime = 0.75f;
    private float clickPassedTime;
    private float attackTime;
    private bool isCanAttack => (clickPassedTime >= touchCoolTime);
    public bool IsAttackInputIn { get; private set; }
    
    // private Touch tempTouch;

    private void Update()
    {
        // ���� ��� �ð�
        clickPassedTime = Time.time - attackTime;

        if (isEditor)
        {
            // KeyboardInput
            MoveInput();
            if (clickPassedTime > touchCoolTime)
            {
                AttackInput();
            }
        }
        else
        {
            // MobileInput
            TouchInput();
        }
    }

    private void MoveInput()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    private void AttackInput()
    {
        // ������ ��
        if (Input.GetMouseButtonDown(1))
        {
            if (false == IsTouchPlayer()) return;

            IsAttackInputIn = false;
            AimingkEvent?.Invoke();
        }

        // ������ ��
        if (Input.GetMouseButtonUp(1))
        {
            IsAttackInputIn = true;
            attackTime = Time.time;
        }

// #if PLATFORM_ANDROID
// 
//         if (Input.touchCount > 0)
//         {
//             tempTouch = Input.GetTouch(0);
// 
//             if (tempTouch.phase == TouchPhase.Began)
//             {
//                 // �÷��̾� ���� ���� �������� ��Ҵ��� Ȯ��
//                 if (false == IsTouchPlayer()) return;
// 
//                 IsAttackInputIn = false;
//                 AimingkEvent?.Invoke();
//             }
//             if (tempTouch.phase == TouchPhase.Ended)
//             {
//                 IsAttackInputIn = true;
//                 attackTime = Time.time;
//             }
//         }
// 
// #endif
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            // UI ��ġ �� �Լ� ���� ����
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                return;
            }

            Touch tempTouch = Input.GetTouch(0);

            if (tempTouch.phase == TouchPhase.Began)
            {
                HandleTouchBegan();
            }
            if (tempTouch.phase == TouchPhase.Ended)
            {
                HandleTouchEnded();
            }
        }
    }

    private void HandleTouchBegan()
    {
        // �÷��̾� ���� ���� �������� ��Ҵ��� Ȯ���մϴ�.
        if (IsTouchPlayer())
        {
            if (isCanAttack)
            {
                IsAttackInputIn = false;
                AimingkEvent?.Invoke();
            }
        }
        else
        {
            xInput = -1;
        }
    }

    private void HandleTouchEnded()
    {
        if (false == IsAttackInputIn)
        {
            IsAttackInputIn = true;
            attackTime = Time.time;
        }
        xInput = 0;
    }

    private bool IsTouchPlayer()
    {
        bool isTouchPlayer = false;

        RaycastHit2D hit = Physics2D.Raycast(GetTouchPosition(), transform.forward, float.PositiveInfinity);
        if (hit)
        {
            if (hit.transform.gameObject.CompareTag("Player"))
            {
                isTouchPlayer = true;
            }
        }

        return isTouchPlayer;
    }

    public Vector3 GetTouchPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}

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
    public bool IsCanAttack => (clickPassedTime >= touchCoolTime);
    public bool IsThrowReady { get; set; }

    public bool IsActivate { get; set; } = true;

    private void Update()
    {
        if (false == IsActivate) return;

        // 공격 경과 시간
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

    #region PC INPUT

    private void MoveInput()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    private void AttackInput()
    {
        // 눌렀을 때
        if (Input.GetMouseButtonDown(1))
        {
            if (false == IsTouchPlayer()) return;

            IsThrowReady = false;
            AimingkEvent?.Invoke();
        }

        // 떼었을 때
        if (Input.GetMouseButtonUp(1))
        {
            IsThrowReady = true;
            attackTime = Time.time;
        }
    }

    #endregion

    #region MOBILE INPUT

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            // UI 터치 시 함수 실행 종료
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

    public void HandleTouchBegan()
    {
        // 플레이어 쪽을 시작 지점으로 잡았는지 확인합니다.
        if (IsTouchPlayer())
        {
            if (IsCanAttack)
            {
                IsThrowReady = true;
                AimingkEvent?.Invoke();
            }
        }
        else
        {
            xInput = -1;
        }
    }

    public void HandleTouchEnded()
    {
        if (IsThrowReady)
        {
            IsThrowReady = false;
            attackTime = Time.time;
        }
        xInput = 0;
    }

    #endregion

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

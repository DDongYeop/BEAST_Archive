using System;
using System.Collections;
using Behaviour; 
using UnityEngine;

[RequireComponent(typeof(EnemyFeedback), typeof(WeaponStick))]
public class EnemyBrain : MonoBehaviour
{
    [Header("Brain")]
    [HideInInspector] public bool IsDie;

    [Header("Components")] 
    [HideInInspector] public AgentHealth AgentHealth;
    [HideInInspector] public AgentAnimator AgentAnimator;
    [HideInInspector] public WeaponStick WeaponStick;

    [Header("Slow")] 
    private Coroutine _slowCo = null;
    [HideInInspector] public float SlowValue = 100;
    
    [Header("Other")] 
    [HideInInspector] public float StunTime = 0;
    [HideInInspector] public int CurrentNodeValue = -1;
    [HideInInspector] public ActionNode CurrentNode;

    private void Awake()
    {
        AgentHealth = GetComponent<AgentHealth>();
        AgentAnimator = GetComponent<AgentAnimator>();
        WeaponStick = GetComponent<WeaponStick>();
    }

    private void Start()
    {
        SlowValue = 100;
    }

    public void SetVelocityX(float value)
    {
        value *= SlowValue / 100.0f;
        transform.position += new Vector3(value, 0) * Time.deltaTime;
    }

    /// <summary>
    /// 적 감소
    /// </summary>
    /// <param name="time">지속시간</param>
    /// <param name="value">백분율로 나눠진 슬로운 값. 70 == 70% 속도로 감</param>
    public void SetSlow(float time, float value)
    {
        if (_slowCo != null)
            StopCoroutine(_slowCo);
        _slowCo = StartCoroutine(SlowCo(time, value));
    }

    private IEnumerator SlowCo(float time, float value)
    {
        SlowValue = value;
        AgentAnimator.Animator.speed = SlowValue / 100.0f;
        yield return new WaitForSeconds(time);
        SlowValue = 100; 
        AgentAnimator.Animator.speed = 1;
    }
}

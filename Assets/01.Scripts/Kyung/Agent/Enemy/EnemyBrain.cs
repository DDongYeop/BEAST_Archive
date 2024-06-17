using System;
using Behaviour;
using UnityEngine;

[RequireComponent(typeof(BehaviourTreeRunner), typeof(AgentHealth), typeof(WeaponStick))]
public class EnemyBrain : MonoBehaviour
{
    [Header("Brain")]
    [HideInInspector] public bool IsDie;

    [Header("Components")] 
    [HideInInspector] public AgentHealth AgentHealth;
    [HideInInspector] public AgentAnimator AgentAnimator;
    [HideInInspector] public WeaponStick WeaponStick;
    [HideInInspector] public BehaviourTreeRunner Runnder;
    private Rigidbody2D _rigidbody2D;

    [Header("Other")] 
    [HideInInspector] public float StunTime = 0;
    [HideInInspector] public int CurrentNodeValue = -1;
    [HideInInspector] public ActionNode CurrentNode;

    private void Awake()
    {
        AgentHealth = GetComponent<AgentHealth>();
        AgentAnimator = GetComponent<AgentAnimator>();
        WeaponStick = GetComponent<WeaponStick>();
        Runnder = GetComponent<BehaviourTreeRunner>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void SetVelocityX(float value)
    {
        //_rigidbody2D.velocity = new Vector2(value, _rigidbody2D.velocity.y);
        transform.position += new Vector3(value, 0) * Time.deltaTime;
    }
}

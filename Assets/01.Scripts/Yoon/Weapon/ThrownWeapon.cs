using System.Collections;
using UnityEngine;
using System;
using System.ComponentModel;

public class ThrownWeapon : PoolableMono
{
    public ThrownWeaponStat Stat;
    public SkillData SkillData { get; private set; }
    public WeaponSkill Skill { get; private set; }

    private TrailRenderer trail;
    protected Rigidbody2D rigidbody;
    protected Collider2D collider;

    private GameObject targetEnemy => GameManager.Instance.EnemyBrain.transform.root.gameObject;

    private LayerMask weaponLayer;
    private LayerMask garbageLayer;

    private readonly float gravityDefaultValue = 5f;
    private readonly float lifeTimeAfterFall = 10f;

    private Vector3 initScale;
    private float maxForceValue = 112;
    public bool IsFlying;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        trail = transform.Find("Trail").GetComponent<TrailRenderer>();

        weaponLayer = LayerMask.NameToLayer("Weapon");
        garbageLayer = LayerMask.NameToLayer("Garbage");

        initScale = transform.localScale;
        Skill = null;
    }

    public override void Init()
    {
        transform.localScale = initScale;
        gameObject.layer = weaponLayer;
        rigidbody.gravityScale = 0f;
        rigidbody.mass = Stat.WeaponMass;
        IsFlying = false;
    }

    protected virtual void Update()
    {
        if (IsFlying)
        {
            WeaponUpdate();
        }
    }

    protected virtual void WeaponUpdate()
    {

    }

    public void ThrowThisWeapon(Vector2 force, SkillData skillData)
    {
        if (Stat.IsOverThrow)
        {
            Scene_InGame _UI = UIManager_InGame.Instance.GetScene("Scene_InGame") as Scene_InGame;
            PoolManager.Instance.Push(this);
            return;
        }

        if (skillData != null)
        {
            SkillData = skillData;
            CreateSkillComponent(SkillData.SkillType);

            if (SkillData.RunImmediately)
            {
                UseSkill(targetEnemy);
            }
        }

        transform.up = force.normalized;
        rigidbody.AddForce(force * (maxForceValue / rigidbody.mass), ForceMode2D.Impulse);
        rigidbody.gravityScale = gravityDefaultValue;
        IsFlying = true;
        trail.enabled = true;

        Stat.CurrentThrowCount++;
    }

    // ��ų ������Ʈ ����
    private void CreateSkillComponent(SkillType skillName)
    {
        if (Skill == null)
        {
            Type skillType = Type.GetType($"{skillName}Skill");
            Skill = gameObject.AddComponent(skillType) as WeaponSkill;
        }    
    }

    public void UseSkill(GameObject targetObject)
    {
        Skill.UseSkill(targetObject.transform, SkillData);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (false == IsFlying) return;

        if (collision.collider.CompareTag("Ground"))
        {
            OnGroundCollisionEvent();
            PoolManager.Instance.Push(this);
        }
    }

    protected virtual void OnGroundCollisionEvent()
    {
        GameManager.Instance.ComboReset();
        trail.Clear();
    }

    #region ObjectStick

    public void ObjectStick()
    {
        IsFlying = false;
        Destroy(rigidbody);
        collider.enabled = false;
        trail.enabled = false;
    }
    
    public void ObjectFall()
    {
        transform.SetParent(null);
        StartCoroutine(ObjectFallCo());
    }

    private IEnumerator ObjectFallCo()
    {
        yield return new WaitForEndOfFrame();
        gameObject.layer = garbageLayer;
        collider.enabled = true;

        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody2D>();
        }
        rigidbody.velocity = Vector2.zero;

        yield return new WaitForSeconds(lifeTimeAfterFall);
        PoolManager.Instance.Push(this);
    }

    #endregion
}

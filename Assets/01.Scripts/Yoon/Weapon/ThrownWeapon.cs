using System.Collections;
using UnityEngine;
using System;
using System.ComponentModel;

public class ThrownWeapon : PoolableMono
{
    public ThrownWeaponStat Stat;
    public TreasureData TreasureData { get; private set; }
    public TreasureSkill Skill { get; private set; }

    private TrailRenderer trail;
    protected Rigidbody2D rigidbody;
    protected Collider2D collider;
    

    private LayerMask weaponLayer;
    private LayerMask garbageLayer;

    private readonly float gravityDefaultValue = 5f;
    private readonly float lifeTimeAfterFall = 10f;

    private float maxForceValue = 112;
    public bool IsFlying;

    public override void Init()
    {
        if (Skill != null)
        {
            Destroy(Skill);
        }

        gameObject.layer = weaponLayer;
        rigidbody.gravityScale = 0f;
        rigidbody.mass = Stat.WeaponMass;
        IsFlying = false;
    }

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        trail = transform.Find("Trail").GetComponent<TrailRenderer>();

        weaponLayer = LayerMask.NameToLayer("Weapon");
        garbageLayer = LayerMask.NameToLayer("Garbage");
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

    public void ThrowThisWeapon(Vector2 force, TreasureData treasureData)
    {
        if (treasureData != null)
        {
            TreasureData = treasureData;
            CreateSkillComponent(TreasureData.TreasureType);
        }

        transform.up = force.normalized;
        rigidbody.AddForce(force * (maxForceValue / rigidbody.mass), ForceMode2D.Impulse);
        rigidbody.gravityScale = gravityDefaultValue;
        IsFlying = true;
        trail.enabled = true;
    }

    // 스킬 컴포넌트 생성
    private void CreateSkillComponent(TreasureType treasureType)
    {
        Type skillType = Type.GetType($"{treasureType}Component");
        Skill = gameObject.AddComponent(skillType) as TreasureSkill;
    }

    public void UseSkill(GameObject targetObject)
    {
        Skill.UseSkill(targetObject.transform, TreasureData.Damage, TreasureData.Duration);
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

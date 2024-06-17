using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class EnemyCollision : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyPartType _partType = EnemyPartType.SPOT;

    [Header("Component")] 
    private EnemyBrain _brain;
    private AgentHealth _agentHealth;
    private WeaponStick _weaponStick;

    private void Awake()
    {
        _brain = transform.root.GetComponent<EnemyBrain>();
        _agentHealth = _brain.GetComponent<AgentHealth>();
        _weaponStick = _brain.GetComponent<WeaponStick>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_brain.IsDie)
            {
                other.transform.parent.GetComponent<IDamageable>().OnDamage(1, transform.position);
                GameManager.Instance.GameOver();
            }
            return;
        }
        
        if (!other.transform.TryGetComponent(out ThrownWeapon weapon) || !weapon.IsFlying || _brain.IsDie)
            return;
        weapon.IsFlying = false;
        
        if (_partType == EnemyPartType.ATTACK)
        {
            weapon.ObjectFall();
            return;
        }

        if (weapon.Skill != null) // 보물 확인
            weapon.UseSkill(transform.root.gameObject);
        
        PoolManager.Instance.Pop("Bear_Hit");
        
        OnDamage(weapon.Stat.Damage, other.ClosestPoint(transform.position)); // 데미지 주기
        if (!weapon.Stat.IsSharp) // 충돌한 물체가 공격하는 애 인지 확인
        {
            weapon.ObjectFall();
            return;
        }
        
        _weaponStick.AddObj(other.transform, transform); // 박히기
    }

    public void OnDamage(int damage, Vector3 hitPos = new Vector3())
    {
        _agentHealth.OnDamage(damage, hitPos);
        Taptic.Light();
        Transform hitParticle = PoolManager.Instance.Pop("HitEffect").transform;
        hitParticle.position = hitPos;
        Transform furParticle = PoolManager.Instance.Pop("FurEffect").transform;
        furParticle.position = hitPos;
    }
}

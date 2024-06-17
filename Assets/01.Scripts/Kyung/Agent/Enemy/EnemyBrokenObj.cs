using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class EnemyBrokenObj : MonoBehaviour
{
    [SerializeField] private int _maxHp;

    private int _currentHp;

    private void Start()
    {
        _currentHp = _maxHp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.transform.TryGetComponent(out ThrownWeapon weapon) || !weapon.IsFlying)
            return;
        
        weapon.IsFlying = false;
        weapon.ObjectFall();
        PoolManager.Instance.Pop("Bear_Hit");

        if (weapon.Skill != null) // 보물 확인
            weapon.UseSkill(transform.root.gameObject);

        _currentHp -= 1;
        if (_currentHp <= 0)
            BrokenObj();
    }

    private void BrokenObj()
    {
        PoolableMono particle = PoolManager.Instance.Pop("DustEffectBear");
        particle.transform.SetParent(transform.parent);
        particle.transform.localPosition = transform.localPosition;
        Destroy(gameObject);
    }
}

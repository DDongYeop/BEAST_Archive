    using System.Collections;
using UnityEngine;

public class AgentHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthSO _healthSO;

    private EnemyBrain _brain;
    private AgentAnimator _agentAnimator;

    private int _currentHp;
    public int CurrentHp => _currentHp;

    private void Awake()
    {
        _healthSO = Instantiate(_healthSO);
    }

    private void Start()
    {
        _brain = GetComponent<EnemyBrain>();
        _agentAnimator = GetComponent<AgentAnimator>();
        _currentHp = _healthSO.MaxHP;
    }

    public void OnDamage(int damage, Vector3 hitPos)
    {
        _currentHp -= damage;
        _agentAnimator.OnHurt();

        if (_brain.CurrentNode && _brain.CurrentNode.IsAttackStop)
            _brain.AgentAnimator.SetAnimEnd();

        if (_currentHp <= 0)
            StartCoroutine(EnemyDie());
        
        // Damage Text 
        Vector3 pos = hitPos == Vector3.zero ? _brain.transform.position : hitPos;
        Color color = hitPos == Vector3.zero ? Color.red : Color.white;
        (PoolManager.Instance.Pop("DamagePopup") as DamagePopup)?.SetUp("-" + damage.ToString(), pos, 60, color);
        (UIManager_InGame.Instance.GetScene("Scene_InGame") as Scene_InGame)?.OnEnemyHPChanged(HpPercent());
    }

    /// <summary>
    /// 0.0f와 1.0f의 사이 값이 나옵니다 .나중에 HPbar을 만들때 쓰기 좋을거에요. 
    /// </summary>
    /// <returns></returns>
    public float HpPercent()
    {
        return (float)_currentHp / (float)_healthSO.MaxHP;
    }

    private IEnumerator EnemyDie()
    {
        GameManager.Instance.GameClear();
        _brain.IsDie = true;
        _agentAnimator.OnDie();
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void Stun(float maxTime)
    {
        if (_brain.StunTime <= 0)
        {
            _brain.StunTime = maxTime;
            _agentAnimator.SetAnimEnd();
        }
    }
}

//도트댐 만들기 

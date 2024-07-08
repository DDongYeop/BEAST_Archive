using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    [Header("Animator")] 
    [HideInInspector] public Animator Animator;
    
    [Header("Hash")]
    private readonly int _hurtHash = Animator.StringToHash("IsHurt");
    private readonly int _dieHash = Animator.StringToHash("IsDie");
    private readonly int _stunHash = Animator.StringToHash("IsStun");

    [Header("Other")]
    protected EnemyBrain _brain;
    private WeaponStick _weaponStick;
    private EnemyFeedback _enemyFeedback;

    protected virtual void Awake()
    {
        _brain = GetComponent<EnemyBrain>();
        Animator = GetComponent<Animator>();
        _weaponStick = GetComponent<WeaponStick>();
        _enemyFeedback = GetComponent<EnemyFeedback>();
    }

    public void OnHurt() => Animator.SetTrigger(_hurtHash);
    public void OnDie() => Animator.SetBool(_dieHash, true);
    public void Onstun(bool value) => Animator.SetBool(_stunHash, value);
    
    #region Ohter

    public void OnOtherTrigger(int hash) => Animator.SetTrigger(hash);
    public void OnOtherTrigger(string hash) => Animator.SetTrigger(hash);
    
    public void OnOtherBool(int hash, bool value) => Animator.SetBool(hash, value);
    public void OnOtherBool(string hash, bool value) => Animator.SetBool(hash, value);
    
    public void OnOtherInt(int hash, int value) => Animator.SetInteger(hash, value);
    public void OnOtherInt(string hash, int value) => Animator.SetInteger(hash, value);
    
    public void OnOtherFloat(int hash, float value) => Animator.SetFloat(hash, value);
    public void OnOtherFloat(string hash, float value) => Animator.SetFloat(hash, value);

    #endregion

    #region Event

    public virtual void SetAnimEnd()
    {
        _enemyFeedback.ShowAttackTrailFalse();
        
        //if (_brain.CurrentNode)
        //    _brain.CurrentNode.OnStop();
        //_brain.CurrentNodeValue = -1;
    }

    public void RemoveWeaponStick()
    {
        _weaponStick.RemoveObj();
    }

    #endregion
}

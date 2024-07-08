using System.Collections.Generic;
using UnityEngine;

public class EnemyFeedback : MonoBehaviour
{
    [SerializeField] private string _currentBoss;
    [SerializeField] private List<TrailRenderer> _trails = new List<TrailRenderer>();

    [SerializeField] private List<Vector2> _dustEffectPos;
    [SerializeField] private Vector3 _spiderWebPos;

    private void Start()
    {
        ShowAttackTrailFalse();
    }

    public void SoundPlay(string str)
    {
        PoolManager.Instance.Pop(str);
    }

#region AnimationEvent

    public void ShowAttackTrailTrue()
    {
        foreach (var trail in _trails)
            trail.enabled = true;
    }

    public void ShowAttackTrailFalse()
    {
        foreach (var trail in _trails)
            trail.enabled = false;
    }

    public void CameraShake()
    {
        CameraManager.Instance.CameraShake(1f, 0.25f);
    }

    public void DustEffect(int num)
    {
        Transform trm;

        if (num < 0)
            trm = PoolManager.Instance.Pop($"DustEffect{_currentBoss}Weak").transform;
        else 
            trm = PoolManager.Instance.Pop($"DustEffect{_currentBoss}").transform;
        
        trm.SetParent(transform);
        trm.localPosition = _dustEffectPos[Mathf.Abs(num)];
    }

    public void SpiderWeb(float x)
    {
        print(x);
        global::SpiderWeb web = PoolManager.Instance.Pop("SpiderWeb") as SpiderWeb;
        web.transform.position = transform.position + _spiderWebPos;
        web.TargetPos = GameManager.Instance.PlayerTrm.position + new Vector3(x, 0);
    }

#endregion
}

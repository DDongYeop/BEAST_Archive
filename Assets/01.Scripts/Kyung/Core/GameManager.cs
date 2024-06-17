using System;
using UnityEngine;
using TMPro;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Player")] 
    public Transform PlayerTrm;

    [Header("Pooling")] 
    [SerializeField] private PoolingListSO _poolingList;
    [SerializeField] private PoolingListSO _weaponPoolingList;

    private float _playTime;
    public bool IsGameOver = false;

    public float PlayTime
    {
        get { return Mathf.Floor(_playTime * 10f) / 10f; }
        set { _playTime = value; }
    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        PlayerTrm = GameObject.Find("Player").transform;
        CreatePool();

        // Manager Script Init
        CameraManager.Instance.Init();
        SaveLoadManager.Instance.Init();
    }

    private void Start()
    {
        FrameLimit();
    }

    private void FixedUpdate()
    {
        _playTime += Time.fixedDeltaTime;
    }

    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _poolingList.PoolList.ForEach(p => PoolManager.Instance.CreatePool(p.Prefab, p.Count));
        _weaponPoolingList.PoolList.ForEach(p => PoolManager.Instance.CreatePool(p.Prefab, p.Count));
    }

    [ContextMenu("GameClear")]
    public void GameClear()
    {
        Debug.Log("Game Clear");
        UIManager_InGame.Instance.GetScene("Scene_OnEnd").GetComponent<Scene_OnEnd>().OnGameClear();
    }
    
    public void GameOver()
    {
        if (IsGameOver)
            return;

        IsGameOver = true;
        FindObjectOfType<EnemyBrain>().AgentAnimator.SetAnimEnd();
        Debug.Log("Game Over");
        UIManager_InGame.Instance.GetScene("Scene_OnEnd").GetComponent<Scene_OnEnd>().OnGameOver();
    }

    private void FrameLimit()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 120;
#endif
    }
}

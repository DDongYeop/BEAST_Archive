using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoSingleton<LevelManager>
{
    [SerializeField] private int _currentLevelValue;
    public SkillInfo _newSkill { private set; get; }

    public override void Init()
    {
        base.Init();
        // Not Walk
    }

    public void GameClear()
    {
        if (!SaveLoadManager.Instance.data.levels[_currentLevelValue].Clear)
        {
            //첫 클리어
            List<int> notActiveIndex = new List<int>();
            
            for (int i = 0; i < SaveLoadManager.Instance.data.skillInfoList.Count; ++i)
            {
                if (SaveLoadManager.Instance.data.skillInfoList[i].IsActive)
                    continue;
                
                notActiveIndex.Add(i);
            }

            if (notActiveIndex.Count != 0) //다 활성화 된 상황이 아닐때. 
            {
                int addIndex = Random.Range(0, notActiveIndex.Count);
                _newSkill = SaveLoadManager.Instance.data.skillInfoList[addIndex];
                SaveLoadManager.Instance.data.skillInfoList[addIndex].IsActive = true; //획득
            }
        }

        SaveLoadManager.Instance.data.levels[_currentLevelValue].Clear = true;
        SaveLoadManager.Instance.SaveData();
    }
}

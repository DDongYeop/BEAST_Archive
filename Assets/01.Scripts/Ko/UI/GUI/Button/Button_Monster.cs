using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Monster : Button_Menu
{
    [SerializeField] private StageSO _stageData;

    public override void OnAction()
    {
        SaveLoadManager.Instance.LoadData();
        UIManager_Menu.Instance.ShowScene("Image_Stages");
        UIManager_Menu.Instance.HideScene("Panel_Menu");
        
        (UIManager_Menu.Instance.GetScene("Image_Stages") as Scene_LevelSelect).BindSlot(_stageData);
    }
}

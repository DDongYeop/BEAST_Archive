using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene_BookPage2 : UI_Scene
{
	protected override void Init()
	{
		base.Init();

        SelectState selectState = FindObjectOfType<SelectState>();
        Get<Button>("PlayButton").onClick.AddListener(selectState.NextState);

        BindEvent(Get<Image>("Image_Exit").gameObject, (PointerEventData _data, Transform _transform) => { UIManager_Menu.Instance.SetPage(1); });

    }

    public void SetStageInfo(Sprite _mapSprite, Sprite _bossSprite, string _stageName, string _stageInfo)
    {
        Image _mapImage = Get<Image>("Image_MapInfo");
        _mapImage.sprite = _mapSprite;
        _mapImage.preserveAspect = true;

        Image _bossImage = Get<Image>("Image_BossInfo");
        _bossImage.sprite = _bossSprite;
        _bossImage.preserveAspect = true;

        Get<TextMeshProUGUI>("Text_StageName").text = _stageName;
        Get<TextMeshProUGUI>("Text_StageInfo").text = _stageInfo;
    }
}

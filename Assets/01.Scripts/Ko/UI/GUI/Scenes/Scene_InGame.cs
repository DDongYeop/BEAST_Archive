using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Linq;

public class Scene_InGame : UI_Scene, IDataObserver
{
    [SerializeField] private TreasureInfo[] _treasureInfo;
    private List<ThrownWeaponInfo> _weaponInfos = new List<ThrownWeaponInfo>();
    private ThrownWeaponInfo _currentItem = null;

    [SerializeField] private WeaponController weaponController;

    protected override void Init()
    {
        base.Init();
        //Get<Button>("AAA").onClick.AddListener(InitItem);
    }
    
    protected override void Start()
    {
        weaponController = GameManager.Instance.PlayerTrm.Find("WeaponController").GetComponent<WeaponController>();
        InitItem();
    }

    public void SetTreasureUI(string _name)
    {
        var _newSprite = (from item in _treasureInfo where item.TreasureIdName == _name select item.TreasureSprite);
        Get<Image>("Image_TreasureIcon").sprite = _newSprite.First();
    }
    
    private void InitItem()
    {
        for (int i = 0; i < 3; i++)
        {
            BindEvent(Get<Image>($"Image_ItemSelector{i + 1}").gameObject, OnSelectItem, Define.ClickType.Click);

            Image _image = Get<Image>($"Image_ItemIcon{i + 1}");
            //_image.rectTransform.anchoredPosition = _weaponInfos[i - 1].SpritePivotPosition;
            _image.sprite = _weaponInfos[i].WeaponSprite;
            _image.preserveAspect = true;
        }

        weaponController.AttemptChangeWeaponStat(_weaponInfos[0].WeaponId);
        //GameObject.Find("Player").GetComponent<PlayerAttack>()
    }

    public void OnThrow(int count, int max)
    {
        Get<TextMeshProUGUI>("Text_TreasureCount").text =  $"{count}/{max}";
    }

    private void OnSelectItem(PointerEventData _data, Transform _transform)
    {
        var itemIndex = _transform.name.Split('r');
        Debug.Log($"{itemIndex[1]} is Cliked");

        weaponController.AttemptChangeWeaponStat(_weaponInfos[int.Parse(itemIndex[1]) - 1].WeaponId);


        var selector = Get<Image>($"Image_ItemSelector{int.Parse(itemIndex[1])}");
        selector.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.2f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            selector.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InSine);
        });


        if(_currentItem != _weaponInfos[int.Parse(itemIndex[1]) - 1])
        {
            StopAllCoroutines();
            StartCoroutine(ItemPopup(_weaponInfos[int.Parse(itemIndex[1]) - 1]));
        }


        _currentItem = _weaponInfos[int.Parse(itemIndex[1]) - 1];
        //selector.rectTransform.DOLocalMoveY(-45, 0.5f).SetEase(Ease.OutExpo).OnComplete(() =>
        //{
        //    selector.rectTransform.DOLocalMoveY(-66, 0.5f).SetEase(Ease.OutSine);
        //});
    }

    private IEnumerator ItemPopup(ThrownWeaponInfo _data)
    {
        GameObject _itemPanel = Get<Image>("Image_CurrentItem").gameObject;

        if(_itemPanel.transform.Find("Image_ItemIcon").TryGetComponent<Image>(out Image _image))
        {
            _image.sprite = _data.WeaponSprite;
            _image.preserveAspect = true;
            //_image.rectTransform.anchoredPosition = _data.SpritePivotPosition;
        }


        _itemPanel.transform.Find("Text_ItemName").GetComponent<TextMeshProUGUI>().text = _data.WeaponName;


        _itemPanel.GetComponent<RectTransform>().DOAnchorPosX(0, 0.6f).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(0.75f);

        _itemPanel.GetComponent<RectTransform>().DOAnchorPosX(200, 0.3f);
    }

    public void OnEnemyHPChanged(float percent)
    {
        DOTween.To(() => Get<Slider>("Slider_EnmyHP").value, NewValue => Get<Slider>("Slider_EnmyHP").value = NewValue, percent, 0.2f);
    }

    private IEnumerator LearpRectPositionRoutine(RectTransform _obj, Vector3 _startPos, Vector3 _endPos)
    {
        float _timeStartedLerping = 0;

        while (true)
        {
            _timeStartedLerping += Time.deltaTime;

            _obj.transform.localPosition = Vector3.Lerp(_startPos, _endPos, _timeStartedLerping / 1);

            if (_timeStartedLerping >= 1.0f)
            {
                break;
            }
        }
        yield return null;
    }

    public void WriteData(ref SaveData data)
    {

    }

    public void ReadData(SaveData data)
    {
        _weaponInfos = data.weaponInfoList;
    }
}

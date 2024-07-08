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
    private SkillInfo _skillInfo;
    private List<ThrownWeaponInfo> _weaponInfos = new List<ThrownWeaponInfo>();
    private ThrownWeaponInfo _currentItem = null;
    private Image _currentSlotUI = null;

    [SerializeField] private WeaponController weaponController;

    protected override void Init()
    {
        base.Init();

        //Get<Button>("AAA").onClick.AddListener(InitItem);
    }

    protected override void Start()
    {
        SaveLoadManager.Instance.LoadData();

        weaponController = GameManager.Instance.PlayerTrm.Find("WeaponController").GetComponent<WeaponController>();
        weaponController.AttemptChangeSKillData(_skillInfo.SkillType);
        Get<Image>("Image_TreasureIcon").sprite = _skillInfo.SkillSprite;
        InitItem();

        //Get<Image>("Image_TreasureIcon").sprite = _skillInfo.Where(i => i.SkillType == ).
    }

    //public void SetTreasureUI(string _name)
    //{
    //    var _newSprite = (from item in _skillInfo where item.SkillId == _name select item.SkillSprite);
    //    Get<Image>("Image_TreasureIcon").sprite = _newSprite.First();
    //}
    
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
    }

    public void OnThrow(int count, int max)
    {
        Get<TextMeshProUGUI>("Text_TreasureCount").text =  $"{count}/{max}";
    }

    public void SetChain(bool _enable) 
    {
        _currentSlotUI.transform.Find("Image_Backgorund").Find("Image_Chain").gameObject.SetActive(_enable);
    }

    private void OnSelectItem(PointerEventData _data, Transform _transform)
    {
        var itemIndex = _transform.name.Split('r');
        Debug.Log($"{itemIndex[1]} is Cliked");

        var selector = Get<Image>($"Image_ItemSelector{int.Parse(itemIndex[1])}");

        selector.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.2f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            selector.transform.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InSine);
        });

        weaponController.AttemptChangeWeaponStat(_weaponInfos[int.Parse(itemIndex[1]) - 1].WeaponId);


        if(_currentItem != _weaponInfos[int.Parse(itemIndex[1]) - 1])
        {
            StopAllCoroutines();
            StartCoroutine(ItemPopup(_weaponInfos[int.Parse(itemIndex[1]) - 1]));
        }


        if(_currentSlotUI != null)
        {
            if(_currentSlotUI.transform.Find("Image_Popup").TryGetComponent(out RectTransform _rect))
            {
                if(_rect.anchoredPosition.y != 0)
                {
                    _rect.DOAnchorPosY(0, 0.5f);
                }
            }
        }

        if (weaponController.CurrentWeaponStat.MaxThrowCount != 0)
        {
            if(selector.transform.Find("Image_Popup").TryGetComponent(out RectTransform _rect))
            {
                if(_rect.transform.Find("Text_Popup").TryGetComponent(out TextMeshProUGUI _text))
                {
                    _text.text = $"{weaponController.CurrentWeaponStat.CurrentThrowCount}/{weaponController.CurrentWeaponStat.MaxThrowCount}";
                }

                _rect.DOAnchorPosY(13, 0.5f);
            }
        }

        _currentItem = _weaponInfos[int.Parse(itemIndex[1]) - 1];
        _currentSlotUI = selector;
    }

    public void OnUtillWeaponThrowed(int _current, int _max)
    {
        if(_currentSlotUI.transform.Find("Image_Popup").Find("Text_Popup").TryGetComponent(out TextMeshProUGUI _text))
        {
            _text.text = $"{_current}/{_max}";
        }
    }

    public IEnumerator ItemPopup(ThrownWeaponInfo _data = null, string _text = null)
    {
        GameObject _itemPanel = Get<Image>("Image_CurrentItem").gameObject;

        if(_itemPanel.transform.Find("Image_ItemIcon").TryGetComponent<Image>(out Image _image))
        {
            _image.sprite = _data?.WeaponSprite ?? _currentItem.WeaponSprite;
            _image.preserveAspect = true;
        }

        _itemPanel.transform.Find("Text_ItemName").GetComponent<TextMeshProUGUI>().text = (string.IsNullOrEmpty(_text) ? _data?.WeaponName : _text);

        _itemPanel.GetComponent<RectTransform>().DOAnchorPosX(5, 0.6f).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(0.85f);
        _itemPanel.GetComponent<RectTransform>().DOAnchorPosX(400, 0.3f);
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

    public void ApplyCombo(int _combo)
    {
        TextMeshProUGUI _text = Get<TextMeshProUGUI>("Text_Combo");
        if (_combo - 1 <= 1)
        {
            _text.DOFade(0, 0.3f);
            return;
        }

        if(_text.color != Color.white)
        {
            _text.DOFade(1, 0.3f);
        }

        _text.text = $"{_combo - 1}\n<size=20>COMBO</size>";
        //_text.rectTransform.DOShakePosition(0.5f, 0.9f);
        _text.rectTransform.DOShakeScale(0.5f, 0.4f);
        //_text.transform.DOScale(new Vector3(1.35f, 1.35f, 1), 0.3f).SetEase(Ease.OutElastic).OnComplete(() =>
        //{
        //    _text.transform.DOScale(new Vector3(1, 1, 1), 0.1f);
        //});
    }

    public void WriteData(ref SaveData data)
    {

    }

    public void ReadData(SaveData data)
    {
        _weaponInfos = data.weaponInfoList;
        _skillInfo = data.SkillInfo;
    }
}

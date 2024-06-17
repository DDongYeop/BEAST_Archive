using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


class ItemSlotUI
{
    public ItemSlotUI(ItemDataSO so, GameObject obj, int index)
    {

    }
}

public class Scene_BookPage4 : UI_Scene
{
    [Header("Reference")]
    [SerializeField] private GameObject _inventorySlotObj;
    [SerializeField] private Sprite _noramlIcon;


    [SerializeField] private ItemDataSO[] _itemDataSOTest;
    private ItemDataSO[] _itemDataSOList;
    private ItemDataSO _curItem = null;
    private int _curIndex = -1;


    protected override void Start()
    {
        ClearItemSlot();
        MakeItemSlotUI(_itemDataSOTest);

        BindEvent(Get<Image>("Image_Exit").gameObject, (PointerEventData _data, Transform _transform) => { UIManager_Menu.Instance.SetPage(1); });

    }

    private void ClearItemSlot()
    {
        _curItem = null;
        _curIndex = -1;

        var _panel = Get<Image>("Panel_Inventory").gameObject;
        
        for(int i = 0; i < _panel.transform.childCount; i++)
        {
            Destroy(_panel.transform.GetChild(i).gameObject);
        }


        Get<TextMeshProUGUI>("Text_ItemName").text = "";
        Get<Image>("Image_Icon").sprite = _noramlIcon;
        Get<TextMeshProUGUI>("Text_ItemInfo").text = "아이템을 선택해 보세요";
    }

    public void MakeItemSlotUI(ItemDataSO[] _itemDataSO)
    {
        ClearItemSlot();

        var _panel = Get<Image>("Panel_Inventory").gameObject;
        _itemDataSOList = _itemDataSO;

        for (int i = 0; i < _itemDataSOList.Length; i++)
        {
            GameObject _newSlot = Instantiate(_inventorySlotObj, _panel.transform);
            _newSlot.name += "@" + i.ToString();

            _newSlot.transform.Find("Image_Icon").GetComponent<Image>().sprite = _itemDataSOList[i].itemImage;
            _newSlot.transform.Find("Text_ItemCount").GetComponent<TextMeshProUGUI>().text = "0";
            BindEvent(_newSlot, OnSelectItem, Define.ClickType.Click);
        }

        Bind<Image>();
        Bind<TextMeshProUGUI>();
        Bind<Button>();
        Bind<Slider>();
    }

    private void OnSelectItem(PointerEventData data, Transform transform)
    {
        if(_curItem != null)
        {
            Get<Image>("Image_InventorySlot(Clone)@" + _curIndex.ToString()).transform.Find("Image_Selected").gameObject.SetActive(false);
        }

        string[] _str = transform.name.Split('@');
        _curIndex = int.Parse(_str[1]);
        _curItem = _itemDataSOList[_curIndex];
        Get<Image>("Image_InventorySlot(Clone)@" + _str[1]).transform.Find("Image_Selected").gameObject.SetActive(true);


        Get<TextMeshProUGUI>("Text_ItemName").text = _curItem.itemName;
        Get<Image>("Image_Icon").sprite = _curItem.itemImage;
        Get<TextMeshProUGUI>("Text_ItemInfo").text = _curItem.description;
    }
}

using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[Serializable]
//struct TechTrees
//{
//    public GameObject TreePanel;
//    public Vector2 pos;
//}

public class Scene_BookPage3 : UI_Scene
{
    [SerializeField] private float _swipMaxDistance = 3f;

    private Vector2 _currentVec = Vector2.zero;

    private float _techTreeWidth => Get<Image>("Image_TechTree").rectTransform.sizeDelta.x;
    private float _techTreeHeight => Get<Image>("Image_TechTree").rectTransform.sizeDelta.y;

    private Vector2 _touchStartPos;
    [SerializeField] private float _moveSpeed = 0.5f;

    protected override void Init()
    {
        base.Init();

        //foreach (TechTrees _tech in list)
        //{
        //    var _events = _tech.TreePanel.GetComponentsInChildren<UI_EventHandler>();

        //    foreach (UI_EventHandler _event in _events)
        //    {
        //        BindEvent(_event.gameObject, OnArrowDwon);
        //    }

        //    //_tech.TreePanel.gameObject.SetActive(false);
        //}

        BindEvent(Get<Image>("Panel_TechTree").gameObject, (PointerEventData _data, Transform _transform) => TechTreeControll(_data, _transform, Define.ClickType.Down), Define.ClickType.Down);
        BindEvent(Get<Image>("Panel_TechTree").gameObject, (PointerEventData _data, Transform _transform) => TechTreeControll(_data, _transform, Define.ClickType.Move), Define.ClickType.Move);
        BindEvent(Get<Image>("Panel_TechTree").gameObject, (PointerEventData _data, Transform _transform) => TechTreeControll(_data, _transform, Define.ClickType.Move), Define.ClickType.Up);
        BindEvent(Get<Image>("Image_Exit").gameObject, (PointerEventData _data, Transform _transform) => { UIManager_Menu.Instance.SetPage(1); });

    }

    bool isDown = false;
    private void TechTreeControll(PointerEventData _data, Transform _transform, Define.ClickType _clickType)
    {

        if (_clickType == Define.ClickType.Up)
        {
            isDown = false;
            return;
        }

        if (_clickType == Define.ClickType.Down)
        {
            _touchStartPos = _data.position;
            isDown = true;
        }
        else if(_clickType == Define.ClickType.Move && isDown)
        {
            Vector2 dir = _touchStartPos - _data.position;
            dir *= _moveSpeed;
            
            Image _techTree = Get<Image>("Image_TechTree");


            Vector3 _totalPos = new Vector3
                    (
                    Mathf.Clamp(_techTree.rectTransform.anchoredPosition.x - dir.x, -371.4f, 0),
                    Mathf.Clamp(_techTree.rectTransform.anchoredPosition.y - dir.y, 0, 376.295f),
                    0
                    );

            Get<Image>("Image_TechTree").rectTransform.anchoredPosition = _totalPos;
            //DOTween.KillAll();
            //Get<Image>("Image_TechTree").rectTransform.DOAnchorPos(_totalPos, 0.01f);
        }
    }

    protected override void Update()
    {
        base.Update();

        //TouchInput();
    }



    private void TouchInput()
    {
        if(Input.touchCount == 1)
        {
            Touch _touch = Input.GetTouch(0);

            if(_touch.phase == TouchPhase.Began)
            {
                _touchStartPos = _touch.position;
            }
            else if(_touch.phase == TouchPhase.Moved)
            {
                Vector2 _vecTouchSize = _touch.position - _touchStartPos;



                //Vector3 _totalPos = new Vector3
                //    (
                //    Mathf.Clamp(_techTree.rectTransform.anchoredPosition.x + _vecTouchSize.x, 0, _techTreeWidth),
                //    Mathf.Clamp(_techTree.rectTransform.anchoredPosition.y + _vecTouchSize.y, 0, _techTreeHeight),
                //    0
                //    );

                //Debug.Log($"x : {_techTree.rectTransform.anchoredPosition.x}  y : {_techTree.rectTransform.anchoredPosition.y}");
                //Get<Image>("Image_TechTree").rectTransform.position = _totalPos;
                //Get<Image>("Image_TechTree").rectTransform.DOMove(_totalPos, 0.3f);
            }
            
        }

    }

    private void OnArrowDwon(PointerEventData _data, Transform _transform)
    {
        Vector2 vec = Vector2.zero;
        string[] _name = _transform.name.Split("w");

        Debug.Log(_transform.name);


        switch (_name[1])
        {
            case "Down":
                vec = Vector2.down;
                break;
            case "Up":
                vec = Vector2.up;
                break;
            case "Left":
                vec = Vector2.left;
                break;
            case "Right":
                vec = Vector2.right;
                break;
        }
        //if (_name == "Image_ArrowDown")
        //{
        //    vec = Vector2.down;
        //    Debug.Log("IsContaine Down");
        //}
        //else if (_name == "Image_ArrowUp")
        //{
        //    vec = Vector2.up;
        //    Debug.Log("IsContaine Up");

        //}
        //else if (_name == "Image_ArrowLeft")
        //{
        //    vec = Vector2.left;
        //    Debug.Log("IsContaine Left");

        //}
        //else if (_name == "Image_ArrowRight")
        //{
        //    vec = Vector2.right;
        //    Debug.Log("IsContaine Right");

        //}


        _currentVec += vec;

     
    }
}

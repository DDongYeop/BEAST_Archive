using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
struct Levels
{
    [SerializeField] private Sprite m_sprite;
    public Sprite Sprite => m_sprite;

    [SerializeField] private string m_name;
    public string Name => m_name;

    [SerializeField] private string m_sceneName;
    public string SceneName => m_sceneName;
}

public class Scene_Menu : UI_Scene
{
    //[SerializeField] private Levels[] _levels;
    [SerializeField] private Image _slotObj;
    [SerializeField] private float _snapForce;
    [SerializeField] private Vector2 _slotSize;
    [SerializeField] private float _fadeDuration = 0.5f;

    private bool _btnSelect = false;
    private bool _isMouseDown = false;
    private bool _isSnapped = true;
    private float _snapSpeed = 0;
    private ScrollRect _scrollRect;
    private RectTransform _contentPanel;
    private HorizontalLayoutGroup _horGroup;

    private int _curIndex = -1;

    protected override void Init()
    {
        base.Init();

        Bind<HorizontalLayoutGroup>();
        _scrollRect = Get<ScrollRect>("ScrollRect_Levels");
        _horGroup = Get<HorizontalLayoutGroup>("Content");
        _contentPanel = _horGroup.gameObject.GetComponent<RectTransform>(); 

        _scrollRect.onValueChanged.AddListener(OnScrollMove);
        
        //_scrollRect.OnInitializePotentialDrag

        BindEvent(Get<ScrollRect>("ScrollRect_Levels").gameObject, (PointerEventData _data, Transform _transform) => { _isMouseDown = true; }, Define.ClickType.Down);
        //BindEvent(Get<ScrollRect>("ScrollRect_Levels").gameObject, (PointerEventData _data, Transform _transform) => { _isMouseDown = true; }, Define.ClickType.Move);
        BindEvent(Get<ScrollRect>("ScrollRect_Levels").gameObject, (PointerEventData _data, Transform _transform) => { _isMouseDown = false; }, Define.ClickType.Up);
        //BindEvent(_scrollRect.gameObject, (PointerEventData _data, Transform _transform) => { _isMouseDown = true; }, Define.ClickType.OnDrag);
        //BindEvent(_scrollRect.gameObject, (PointerEventData _data, Transform _transform) => { _isMouseDown = false; }, Define.ClickType.EndDrag);
        //BindSlot();

        _btnSelect = true;
    }

    protected override void Update()
    {
        //base.Update();

        //Debug.Log(_isMouseDown);

        //_curIndex = Mathf.RoundToInt(0 - _contentPanel.localPosition.x / (_slotSize.x + _horGroup.spacing));

        //if (_scrollRect.velocity.magnitude < 200 && !_isSnapped && !_isMouseDown)
        //{
        //    _scrollRect.velocity = Vector2.zero;
        //    _snapSpeed += _snapForce * Time.deltaTime;

        //    _contentPanel.localPosition = new Vector3(
        //        Mathf.MoveTowards(_contentPanel.localPosition.x, 0 - (_curIndex * (_slotSize.x + _horGroup.spacing)), _snapSpeed),
        //        _contentPanel.localPosition.y,
        //        _contentPanel.localPosition.z);


        //    if (_contentPanel.localPosition.x == 0 - (_curIndex * (_slotSize.x + _horGroup.spacing)))
        //    {
        //        _isSnapped = true;
        //    }
        //}

        //if (_scrollRect.velocity.magnitude > 200)
        //{
        //    _isSnapped = false;
        //    _snapSpeed = 0;
        //}
    }


    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //SaveLoadManager.Instance.LoadData();
        StartCoroutine(EnableRoutine());    
    }

    protected IEnumerator EnableRoutine()
    {
        yield return new WaitForSeconds(0.1f);

        Button_Menu[] _button_Menus = _contentPanel.GetComponentsInChildren<Button_Menu>();

        foreach(Button_Menu _button in _button_Menus)
        {
            BindEvent(_button.gameObject, OnButtonSelected, Define.ClickType.Click);
            _button.ShowUI();
        }
    }

    //private void BindSlot()
    //{
    //    DestroySlot();

    //    foreach(Levels _level in _levels)
    //    {
    //        GameObject _newSlot = Instantiate(_slotObj.gameObject, _contentPanel.transform);

    //        Transform _fill = _newSlot.transform.Find("Image_Fill");
    //        _fill.transform.Find("Text_Name").GetComponent<TextMeshProUGUI>().text = _level.Name;
    //        _fill.transform.Find("Image_Mask").GetChild(0).GetComponent<Image>().sprite = _level.Sprite;

    //        _newSlot.name = "Image_" + _level.Name;
    //        BindEvent(_newSlot, (PointerEventData _data, Transform _transform) => { SceneManager.LoadScene(_level.SceneName); });
    //    }
    //}

    //private void OnSceneSelected(PointerEventData _data, Transform _transform)
    //{
    //    string _levelName = _transform.name.Split('_')[1];

    //    var _level = _levels.Where(i => i.Name == _levelName);

    //    //SceneManager.LoadScene(_levelName);
    //}

    private void OnButtonSelected(PointerEventData _data, Transform _transform)
    {
        if (_scrollRect.velocity.magnitude > 100)
            return;

        Debug.Log(_transform.name);
        _btnSelect = false;

        Button_Menu[] _button_Menus = _contentPanel.GetComponentsInChildren<Button_Menu>();
        foreach (Button_Menu _button in _button_Menus)
        {
            _button.HideUI(_fadeDuration);
        }
        
        if(_transform.TryGetComponent(out Button_Menu _menu))
        {
            _menu.ButtonEvent();
        }
    }

    private void DestroySlot()
    {
        foreach (Transform _child in _contentPanel.transform)
        {
            Destroy(_child.gameObject);
        }
    }

    private void OnScrollMove(Vector2 _vec)
    {
        _curIndex = Mathf.RoundToInt(0 - _contentPanel.localPosition.x / (_slotSize.x + _horGroup.spacing));

        if (_scrollRect.velocity.magnitude < 200 && !_isSnapped && !_isMouseDown)
        {
            _scrollRect.velocity = Vector2.zero;
            _snapSpeed += _snapForce * Time.deltaTime;

            _contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(_contentPanel.localPosition.x, 0 - (_curIndex * (_slotSize.x + _horGroup.spacing)), _snapSpeed),
                _contentPanel.localPosition.y,
                _contentPanel.localPosition.z);


            if (_contentPanel.localPosition.x == 0 - (_curIndex * (_slotSize.x + _horGroup.spacing)))
            {
                _isSnapped = true;
            }
        }

        if (_scrollRect.velocity.magnitude > 200)
        {
            _isSnapped = false;
            _snapSpeed = 0;
        }
    }
}


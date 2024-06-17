using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

enum BookState
{
    Closed = 0,
    Page1,
    Page2,
    Page3,
    Page4,
    Page5,
    Page6,
}

public class UIManager_Menu : UI_Root
{
    public static UIManager_Menu Instance;

    [SerializeField] private string _defaultScene = "";

    private readonly string FLIP_LEFT = "FlipLeft";
    private readonly string FLIP_RIGHT = "FlipRight";
    private readonly string CLOSE = "Close";
    private readonly string OPEN = "Open";

    //private Animator _animator;

    private BookState _bookState = BookState.Closed;

    protected override void Awake()
    {
        base.Awake();

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }


        if (PlayerPrefs.HasKey("scene"))
        {
            _defaultScene = PlayerPrefs.GetString("scene");
            PlayerPrefs.DeleteKey("scene");
        }

        foreach (var item in _scenes)
        { 
            if (item.Value.name != _defaultScene)
                item.Value.gameObject.SetActive(false);
        }
    }

    protected override void Start()
    {
        base.Start();


        //BindEvent(Get<Image>("Image_Book").gameObject, BookOpen, Define.ClickType.Click);


        //for(int i = 1; i <= 6; i++)
        //{
        //    //GameObject obj = Get<Image>("Image_ItemIcon" + i.ToString()).gameObject;
        //    //BindEvent(obj, OnSideTapCliked, Define.ClickType.Click);

        //    //obj.SetActive(false);
        //    Get<Image>("Image_Page" + i.ToString()).gameObject.SetActive(false);
        //}
        //Get<Image>("Image_Page" + 0.ToString()).gameObject.SetActive(false);

        //SetPage(1);
    }

    //private void OnSideTapCliked(PointerEventData _data, Transform _transform)
    //{
    //    var itemIndex = _transform.name.Split('n');

    //    SetPage(int.Parse(itemIndex[1]));
    //}

    //private void BookOpen(PointerEventData data = default, Transform transform = default)
    //{
    //    if (_bookState != BookState.Closed)
    //        return;

    //    //_animator.SetTrigger(OPEN);

    //    //SetPage(1);

    //    //Util.Util.BindAnimatorEvent(_animator, "ContentAppear", Util.AniState.OnStart, () => Get<Image>("Image_Page1").gameObject.SetActive(true));
    //    _bookState = BookState.Page1;
    //}

    public void SetPage(int num)
    {
        if (num < 1 || num > 6)
            return;

        Debug.Log("Set Page" + num.ToString());
        //StartCoroutine(PageChangeRoutine(num));

        Get<Image>("Image_Page" + ((int)_bookState).ToString()).gameObject.SetActive(false);
        _bookState = (BookState)num;
        Get<Image>("Image_Page" + ((int)_bookState).ToString()).gameObject.SetActive(true);
    }

    //private IEnumerator PageChangeRoutine(int num)
    //{
    //    yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("Normal"));
    //    _animator.SetTrigger(num > (int)_bookState ? FLIP_LEFT : FLIP_RIGHT);

    //    yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    //    Get<Image>("Image_Page" + ((int)_bookState).ToString()).gameObject.SetActive(false);

    //    yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("ContentAppear"));
    //    //Get<Image>("Image_Page" + 0.ToString()).gameObject.SetActive(true);
    //    _bookState = (BookState)num;
    //    Get<Image>("Image_Page" + ((int)_bookState).ToString()).gameObject.SetActive(true);
    //}
}
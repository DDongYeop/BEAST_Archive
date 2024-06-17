using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button_Level : Button_Menu
{
    [SerializeField] private string _SceneName;

    protected override void Init()
    {
        base.Init();

        GetComponent<Image>().raycastTarget = true;
        BindEvent(gameObject, (PointerEventData _data, Transform _transform) => { OnAction(); });
    }


    public override void OnAction()
    {
        GetComponent<Image>().raycastTarget = false;

        sceneManager.Instance.ChangeSceen(_SceneName, TransitionsEffect.circleWap);
    }
}

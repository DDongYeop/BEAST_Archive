using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene_BookPage5 : UI_Scene
{
    protected override void Start()
    {
        base.Start();

        BindEvent(Get<Image>("Image_Exit").gameObject, (PointerEventData _data, Transform _transform) => { UIManager_Menu.Instance.SetPage(1); });
    }
}

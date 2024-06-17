using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Util;

public class Scene_BookPage1 : UI_Scene
{
    protected override void Start()
    {
        BindEvent(Get<Image>("Panel_Map").gameObject, (PointerEventData point, Transform transform) => { UIManager_Menu.Instance.SetPage(2); }, Define.ClickType.Click);
        BindEvent(Get<Image>("Image_TechTree").gameObject, (PointerEventData point, Transform transform) => { UIManager_Menu.Instance.SetPage(3); });
        BindEvent(Get<Image>("Image_Inventory").gameObject, (PointerEventData point, Transform transform) => { UIManager_Menu.Instance.SetPage(4); });
        BindEvent(Get<Image>("Image_Setting").gameObject, (PointerEventData point, Transform transform) => { UIManager_Menu.Instance.SetPage(5); });
    }
}

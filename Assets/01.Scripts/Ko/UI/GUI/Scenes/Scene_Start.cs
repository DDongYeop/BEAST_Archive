using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scene_Start : UI_Base
{
    protected override void Init()
    {
        base.Init();

        Bind<Image>();
        Bind<TextMeshProUGUI>();


        Get<TextMeshProUGUI>("Text_TaptoPlay").DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo);
        BindEvent(Get<Image>("Image_TouchInput").gameObject, (PointerEventData _data, Transform _transform) => 
        {
            Get<Image>("Image_TouchInput").gameObject.SetActive(false);
            StartCoroutine(StartRoutine());
        });
    }

    private IEnumerator StartRoutine()
    {
        GameObject.Find("01.Bear").GetComponent<Animator>().SetBool("IsAttack01", true);
        yield return new WaitForSeconds(0.6f);
        sceneManager.Instance.ChangeSceen("Menu 2", TransitionsEffect.fade);
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

public class Scene_OnEnd : UI_Scene
{
    [SerializeField] private Sprite _playBtn;
    [SerializeField] private Sprite _rePlayBtn;
    
    protected override void Start()
    {
        base.Start();

        RaycastTarget(true);

        BindEvent(Get<Image>("Image_Map").gameObject, (PointerEventData _data, Transform _transform) =>
        {
            RaycastTarget(false);

            PlayerPrefs.SetString("scene", "Panel_Menu");
            sceneManager.Instance.ChangeSceen("Menu 2", TransitionsEffect.circleWap);
        });

        BindEvent(Get<Image>("Image_Weapon").gameObject, (PointerEventData _data, Transform _transform) =>
        {
            RaycastTarget(false);

            PlayerPrefs.SetString("scene", "Panel_WeaponSelect");
            sceneManager.Instance.ChangeSceen("Menu 2", TransitionsEffect.circleWap);
        });

    }


    protected override void OnActive()
    {
        base.OnActive();

        //StartCoroutine(ActiveSceneRoutine());
    }

    protected override void OnDeactive()
    {
        base.OnDeactive();
    }

    public void OnGameClear()
    {
        StartCoroutine(GameOverRoutine(true));
    }

    public void OnGameOver()
    {
        StartCoroutine(GameOverRoutine(false));
    }

    private IEnumerator GameOverRoutine(bool IsClear)
    {
        //BindEvent(Get<Image>("Image_Play").gameObject, )

        Get<Image>("Image_Play").transform.GetChild(0).GetComponent<Image>().sprite = (IsClear ? _playBtn : _rePlayBtn);

        if (!IsClear)
        {
            Get<TextMeshProUGUI>("Text_VICTORY").text = "Åä¹ú ½ÇÆÐ";
            BindEvent(Get<Image>("Image_Play").gameObject, (PointerEventData _data, Transform _transform) => 
            {
                RaycastTarget(false);
                sceneManager.Instance.ChangeSceen(SceneManager.GetActiveScene().name, TransitionsEffect.circleWap); 
            });
        }
        else
        {
            Get<TextMeshProUGUI>("Text_VICTORY").text = "Åä¹ú ¼º°ø";

            BindEvent(Get<Image>("Image_Play").gameObject, (PointerEventData _data, Transform _transform) =>
            {
                RaycastTarget(false);

                PlayerPrefs.SetString("scene", "Panel_Menu");
                string[] str = SceneManager.GetActiveScene().name.Split('.');
                string _newScene = (int.Parse(str[0]) + 1).ToString() + "." + str[1];
                    if (Application.CanStreamedLevelBeLoaded(_newScene))
                    {
                        PlayerPrefs.SetInt(str[1], (int.Parse(str[0]) + 1));
                        sceneManager.Instance.ChangeSceen(_newScene, TransitionsEffect.circleWap);
                    }
                    else
                    {
                        Get<Image>("Image_Play").transform.GetChild(0).GetComponent<Image>().sprite = _rePlayBtn;
                    }
            });
        }

        if (TryGetComponent(out Image _image))
        {
            _image.color = new Color(0, 0, 0, 0);
            _image.DOFade(0.75f, 0.6f);
        }

        Get<TextMeshProUGUI>("Text_PlayTime").text = "";
        Get<Image>("Image_EndPanel").rectTransform.anchoredPosition = new Vector2(0, -400f);

        float playTime = GameManager.Instance.PlayTime;
        string timeType = (playTime / 60).ToString("F0") + ":" + (playTime % 60).ToString("F0");
        string str = string.Format("{0:D2}:{1:D2}", (int)(playTime / 60), (int)(playTime % 60));

        Get<TextMeshProUGUI>("Text_PlayTime").text = str;

        Get<Image>("Image_EndPanel").rectTransform.DOAnchorPosY(0, 0.5f).onComplete = () =>
        {
            //DOTween.To(() => 0f, x => Get<TextMeshProUGUI>("Text_PlayTime").text = x.ToString("F0"), playTime, 0.8f);
            //DOTween.To(() => "", x => Get<TextMeshProUGUI>("Text_PlayTime").text = x, timeType, 0.8f);
        };


        yield return null;
    }

    private void RaycastTarget(bool _value)
    {
        Get<Image>("Image_Map").raycastTarget = _value;
        Get<Image>("Image_Weapon").raycastTarget = _value;
        Get<Image>("Image_Play").raycastTarget = _value;
    }
}

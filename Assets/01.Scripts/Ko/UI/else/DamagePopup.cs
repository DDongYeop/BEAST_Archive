using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : PoolableMono
{
    private TextMeshProUGUI _textMesh;
    private Camera _cam;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _cam = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SetUp(string text, Vector2 pos, float fontSize = 120f, Color color = default)
    {
        _rectTransform.transform.position = _cam.WorldToScreenPoint(pos);
        //transform.position = pos;
        _textMesh.SetText(text);
        //_textMesh.color = (color == default ? Color.red : color);
        _textMesh.fontSize = fontSize;
        _textMesh.color = color;

        ShowingSequence(2f);
    }

    //public void Update()
    //{
    //    transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    //}

    public void ShowingSequence(float time)
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(_rectTransform.transform.DOMoveY(_rectTransform.transform.position.y + 250, time));
        seq.Join(_textMesh.DOFade(0, 3f));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }
    public override void Init()
    {
        _textMesh.alpha = 1f;
        transform.SetParent(UIManager_InGame.Instance.transform);
    }

}

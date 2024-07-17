using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowableTutorial : TutorialBase
{
    // 2. ���ذ� �߻�
    public override async UniTask ProcessTutorial()
    {
        Debug.Log($"{GetType().Name} Start");

        // ���� ��Ʈ
        await notifyText.DoText(notifyMessageList[0], 1f);

        // CanvasGroup On
        handIconImage.transform.localPosition = squareLineImage.transform.localPosition;
        handIconImage.gameObject.SetActive(true);
        squareLineImage.gameObject.SetActive(true);
        await imageCanvasGroup.DOFade(1f, 1f);

        // Ʃ�丮�� �ȳ�
        handIconImage.DoFadeLoop(3).Forget();
        await notifyText.DoText(notifyMessageList[1], 1f);

        // Ʃ�丮�� ���� Ȯ��
        playerInput.IsActivate = true;
        await UniTask.WaitUntil(() => playerInput.IsThrowReady);
        await UniTask.Delay(1000);

        // CanvasGroup off
        await imageCanvasGroup.DOFade(0f, 1f);
        handIconImage.gameObject.SetActive(false);
        squareLineImage.gameObject.SetActive(false);

        // ������ ��Ʈ
        await notifyText.DoText(notifyMessageList[2], 1f);

        Debug.Log($"{GetType().Name} End");
    }
}

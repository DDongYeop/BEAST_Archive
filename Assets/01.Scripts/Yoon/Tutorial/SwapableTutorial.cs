using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapableTutorial : TutorialBase
{
    public override async UniTask ProcessTutorial()
    {
        Debug.Log($"{GetType().Name} Start");

        // ���� ��Ʈ �� Ʃ�丮�� �ȳ�
        await notifyText.DoText(notifyMessageList[0], 1f);

        // CanvasGroup On
        handIconImage.gameObject.SetActive(false);
        squareLineImage.gameObject.SetActive(true);
        await imageCanvasGroup.DOFade(1f, 1f);

        // Ʃ�丮�� �ȳ�
        squareLineImage.DoFadeLoop(5).Forget();
        await notifyText.DoText(notifyMessageList[1], 1f);
        await notifyText.DoText(notifyMessageList[2], 1f);

        // CanvasGroup off
        await imageCanvasGroup.DOFade(0f, 1f);
        squareLineImage.gameObject.SetActive(false);

        Debug.Log($"{GetType().Name} End");
    }
}

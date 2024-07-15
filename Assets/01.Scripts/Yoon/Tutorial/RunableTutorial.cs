using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class RunableTutorial : TutorialBase
{
    // 1. ������ (�ڷθ�)
    public override async UniTask ProcessTutorial()
    {
        Debug.Log($"{GetType().Name} Start");

        // ������ ��� ����
        await notifyText.DoText(notifyMessageList[0], 1.25f);
        await notifyText.DoText(notifyMessageList[1], 1.25f);

        // CanvasGroup On
        handIconImage.transform.localPosition = squareLineImage.transform.localPosition;
        handIconImage.gameObject.SetActive(true);
        squareLineImage.gameObject.SetActive(true); 
        await imageCanvasGroup.DOFade(1f, 1f);
        
        // Ʃ�丮�� ���� �ȳ� + ��Ʈ
        await notifyText.DoText(notifyMessageList[2], 1.25f);
        handIconImage.DoFadeLoop(3);

        // Ʃ�丮�� ���� Ȯ��
        await this.WaitForTouchInput();

        // CanvasGroup off
        await imageCanvasGroup.DOFade(0f, 1f);
        handIconImage.gameObject.SetActive(false);
        squareLineImage.gameObject.SetActive(false); 

        // ������ ��Ʈ
        await notifyText.DoText(notifyMessageList[3], 1.25f);

        Debug.Log($"{GetType().Name} End");
    }
}

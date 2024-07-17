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
        await notifyText.DoText(notifyMessageList[0], 1f);
        await notifyText.DoText(notifyMessageList[1], 1f);

        // CanvasGroup On
        handIconImage.transform.localPosition = squareLineImage.transform.localPosition;
        handIconImage.gameObject.SetActive(true);
        squareLineImage.gameObject.SetActive(true); 
        await imageCanvasGroup.DOFade(1f, 1f);
        
        // Ʃ�丮�� ���� �ȳ� + ��Ʈ
        handIconImage.DoFadeLoop(3).Forget();
        await notifyText.DoText(notifyMessageList[2], 1f);

        // Ʃ�丮�� ���� Ȯ��
        playerInput.IsActivate = true;
        await UniTask.WaitUntil(() => playerInput.IsMoveInputIn);
        this.DelayAfterAction(1.5f, () =>
        {
            playerInput.HandleTouchEnded();
            playerInput.IsActivate = false;
        }).Forget();

        // CanvasGroup off
        await imageCanvasGroup.DOFade(0f, 1f);
        handIconImage.gameObject.SetActive(false);
        squareLineImage.gameObject.SetActive(false); 

        // ������ ��Ʈ
        await notifyText.DoText(notifyMessageList[3], 1f);

        Debug.Log($"{GetType().Name} End");
    }
}

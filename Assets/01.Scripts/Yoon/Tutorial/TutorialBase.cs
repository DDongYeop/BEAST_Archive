using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public abstract class TutorialBase : MonoBehaviour
{
    protected TextMeshProUGUI notifyText;
    protected CanvasGroup imageCanvasGroup;
    protected Image handIconImage;

    [TextArea(2, 5)]
    [SerializeField] protected List<string> notifyMessageList = new List<string>();

    [SerializeField] protected Image squareLineImage;

    public void SetUIProperty(TutorialController controller)
    {
        notifyText = controller.NotifyText;
        imageCanvasGroup = controller.ImageCanvasGroup;
        handIconImage = controller.HandIconImage;
    }

    public abstract UniTask ProcessTutorial();
}

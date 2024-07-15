using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 3. ���� ���� ����
// 4. �޺� �ý���
// 5. Ʈ��, ȭ���� ���� ��ƿ��Ƽ�� ���� Ȱ���
// 6. �Ʊ���� ���

public class TutorialController : MonoBehaviour
{
    private List<TutorialBase> tutorialList = new ();

    private int currentTutorialIndex = 0;
    private readonly int endTutorialIndex = 7;

    #region UI

    [SerializeField] private TextMeshProUGUI notifyText;
    [SerializeField] private CanvasGroup imageCanvasGroup;
    [SerializeField] private Image handIconImage;

    public TextMeshProUGUI NotifyText => notifyText;
    public CanvasGroup ImageCanvasGroup => imageCanvasGroup;
    public Image HandIconImage => handIconImage;

    #endregion

    private void Awake()
    {
        var tutorialBases = transform.Find("TutorialContainer").GetComponentsInChildren<TutorialBase>();
        tutorialList = tutorialBases.ToList();

        imageCanvasGroup.alpha = 0f;

        foreach (var tutorial in tutorialList)
        {
            if (tutorial != null)
            {
                tutorial.SetUIProperty(this);
            }
        }
    }

    private void Start()
    {
        TutorialRunner().Forget();
    }

    private async UniTaskVoid TutorialRunner()
    {
        await UniTask.Delay(1000);
        await tutorialList[currentTutorialIndex].ProcessTutorial();
        ToNextTutorial();
    }

    private void ToNextTutorial()
    {
        if (currentTutorialIndex == tutorialList.Count - 1)
        {
            // ��� Ʃ�丮�� ��
            Debug.Log("All Tutorial Done");
            return;    
        }

        currentTutorialIndex++;

        TutorialRunner().Forget();
    }
}

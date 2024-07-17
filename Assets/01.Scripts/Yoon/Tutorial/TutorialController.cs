using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TutorialController : MonoBehaviour
{
    private List<TutorialBase> tutorialList = new ();

    private int currentTutorialIndex = 0;

    public PlayerController PlayerController { get; private set; }    

    #region UI

    [SerializeField] private TextMeshProUGUI notifyText;
    [SerializeField] private CanvasGroup imageCanvasGroup;
    [SerializeField] private Image handIconImage;

    public TextMeshProUGUI NotifyText => notifyText;
    public CanvasGroup ImageCanvasGroup => imageCanvasGroup;
    public Image HandIconImage => handIconImage;

    #endregion

    private void OnEnable()
    {
        PlayerController = FindObjectOfType<PlayerController>();

        var tutorialBases = transform.Find("TutorialContainer").GetComponentsInChildren<TutorialBase>();
        tutorialList = tutorialBases.ToList();

        imageCanvasGroup.alpha = 0f;
    }

    private void Start()
    {
        foreach (var tutorial in tutorialList)
        {
            if (tutorial != null)
            {
                tutorial.SetUIProperty(this);
            }
        }

        PlayerController.PlayerInput.IsActivate = false;
        TutorialRunner().Forget();
    }

    private async UniTaskVoid TutorialRunner()
    {
        await UniTask.Delay(500);
        await tutorialList[currentTutorialIndex].ProcessTutorial();
        ToNextTutorial();
    }

    private void ToNextTutorial()
    {
        if (currentTutorialIndex == tutorialList.Count - 1)
        {
            // 모든 튜토리얼 끝
            Debug.Log("All Tutorial Done");
            return;    
        }

        currentTutorialIndex++;

        TutorialRunner().Forget();
    }
}

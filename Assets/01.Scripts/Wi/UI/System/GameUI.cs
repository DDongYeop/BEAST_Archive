using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[Header("Prefab")]
	[SerializeField] private StageInfo stageInfo;

	[Header("References")]
	[SerializeField] private Transform stagesTrm;
	[SerializeField] private StageLineUI lineUI;
	[SerializeField] private CanvasGroup gameUICanvasGroup;

	public void SetVisible(bool show)
	{
		gameUICanvasGroup.alpha = show ? 1f : 0f;
		gameUICanvasGroup.blocksRaycasts = show;
		gameUICanvasGroup.interactable = show;
	}

	#region Stage

	public void CreateStage(StageUIData oldData, StageUIData newData, UnityAction btnHandle)
	{
		StageInfo stageInfoInst = Instantiate(stageInfo, stagesTrm);
		stageInfoInst.SetData(newData, btnHandle);
		newData.info = stageInfoInst;

		Vector2 canvasPos = (Vector2)newData.Position * 50 + Random.insideUnitCircle * 5f;
		stageInfoInst.transform.localPosition = canvasPos;

		if (oldData != null)
		{
			lineUI.DrawLine(oldData, newData);
		}
	}

	#endregion
}

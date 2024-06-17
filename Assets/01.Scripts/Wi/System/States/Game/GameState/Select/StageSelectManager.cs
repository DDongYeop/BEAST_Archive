using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
	[Header("참조")]
    [SerializeField] private GameUI gameUI;
	[SerializeField] private Scene_BookPage2 page2;

	[Header("데이터")]
    [SerializeField] private StageDataSO testData;

	private Dictionary<Vector2Int, StageUIData> datasByPos = new();

	// 초기화 O
	// 활성, 비활성 O
	// 새 스테이지 선택
	// UI O

	private const int BrachRandom = 2;

	private void Awake()
	{
		gameUI.SetVisible(false);
	}

	public void Init()
	{
		// (0, 0) 위치에 기본 스테이지 생성
		StageUIData uiData = new StageUIData() { data = testData };
		uiData.SetData(Vector2Int.zero, Vector2Int.zero);
		datasByPos.Add(uiData.Position, uiData);
		gameUI.CreateStage(null, uiData, () => SetStageInfo(uiData));
	}

	public void Active(bool active)
	{
		gameUI.SetVisible(active);
	}

	private void CreateNewStage(StageUIData parentData)
	{
		int branchCount = Random.Range(1, BrachRandom + 1);
		for (int i = 0; i < branchCount; ++i)
		{
			StageUIData uiData = new StageUIData(parentData) { data = testData };
			Vector2Int direction;

			for (int j = 0; j < 64; ++j)
			{
				direction = parentData.PossibleDir();
				if (datasByPos.ContainsKey(parentData.Position + direction)) continue;

				uiData.SetData(parentData.Position + direction, direction);
				datasByPos.Add(uiData.Position, uiData);
				gameUI.CreateStage(parentData, uiData, () => SetStageInfo(uiData));
				break;
			}
		}
	}

	private void SetStageInfo(StageUIData uiData)
	{
		page2.SetStageInfo(uiData.data.mapData.mapImage, uiData.data.bossData.bossImage, uiData.data.bossData.bossName, "테스트");
	}
}

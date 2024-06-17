using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StageInfo : MonoBehaviour
{
    [SerializeField] private Image mapImage;
    [SerializeField] private Image bossImage;
	[SerializeField] private Button btn;
	[HideInInspector] public Image line;

	private StageUIData stageUIData;

    public void SetData(StageUIData uiData, UnityAction btnHandle)
	{
		stageUIData = uiData;
		btn.onClick.AddListener(btnHandle);
		SetView(stageUIData);
	}

	private void SetView(StageUIData uiData)
	{
		mapImage.sprite = uiData.data.mapData.mapImage;
		bossImage.sprite = uiData.data.bossData.bossImage;
	}
}

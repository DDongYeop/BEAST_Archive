using UnityEngine;
using UnityEngine.UI;

public class StageLineUI : MonoBehaviour
{
	[SerializeField] private Transform lineParent;
    [SerializeField] private Image linePrefab;

	public void DrawLine(StageUIData oldData, StageUIData newData)
	{
		Image line = Instantiate(linePrefab, lineParent);
		Vector3 dir = newData.info.transform.localPosition - oldData.info.transform.localPosition;
		line.transform.localPosition = oldData.info.transform.localPosition + dir * 0.5f;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
		line.transform.rotation = Quaternion.Euler(0, 0, angle);
		(line.transform as RectTransform).sizeDelta = new Vector2(2f, dir.magnitude);
		newData.info.line = line; 
	}
}

using UnityEngine;

public class StageUIData
{
	public StageDataSO data;
	public StageInfo info;

	private Vector2Int direction;
	public Vector2Int Direction => direction;
	private Vector2Int position;
	public Vector2Int Position => position;

	private Vector2Int min = new Vector2Int(-1, -1);
	private Vector2Int max = new Vector2Int(1, 1);
	public Vector2Int Min => min;
	public Vector2Int Max => max;

	public StageUIData() { }
	public StageUIData(StageUIData parentData = null)
	{
		if (parentData != null)
		{
			min = parentData.Min;
			max = parentData.Max;
		}
	}

	public Vector2Int PossibleDir()
	{
		Vector2Int dir = new Vector2Int(Random.Range(min.x, max.x + 1), Random.Range(min.y, max.y + 1));
		while (dir == Vector2Int.zero)
		{
			dir = new Vector2Int(Random.Range(min.x, max.x + 1), Random.Range(min.y, max.y + 1));
		}
		return dir;
	}

	public void SetData(Vector2Int position, Vector2Int direction)
	{
		this.position = position;
		this.direction = direction;
		min += direction;
		min.x = Mathf.Clamp(min.x, -1, 0);
		min.y = Mathf.Clamp(min.y, -1, 0);
		max += direction;
		max.x = Mathf.Clamp(max.x, 0, 1);
		max.y = Mathf.Clamp(max.y, 0, 1);
	}
}

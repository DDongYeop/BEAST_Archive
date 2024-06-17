using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameData/Map")]
public class MapDataSO : ScriptableObject
{
    public Sprite mapImage;
    public GameObject mapPrefab; // GameObject로 되어있지만, 맵의 정확한 구분을 위해서 스크립트를 제작하거나 뭔가를 할 필요가 있음

	private void OnEnable()
	{
		Debug.LogWarning("MapDataSO WARNING: 위인규가 코드 고칠 필요가 있어");
	}
}

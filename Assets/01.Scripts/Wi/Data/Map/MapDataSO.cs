using UnityEngine;

[CreateAssetMenu(menuName = "SO/GameData/Map")]
public class MapDataSO : ScriptableObject
{
    public Sprite mapImage;
    public GameObject mapPrefab; // GameObject�� �Ǿ�������, ���� ��Ȯ�� ������ ���ؼ� ��ũ��Ʈ�� �����ϰų� ������ �� �ʿ䰡 ����

	private void OnEnable()
	{
		Debug.LogWarning("MapDataSO WARNING: ���α԰� �ڵ� ��ĥ �ʿ䰡 �־�");
	}
}

using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
	[SerializeField] private Button gameStartBtn;

	public void Show()
	{
		gameStartBtn.gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameStartBtn.gameObject.SetActive(false);
	}
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragablePanel : MonoBehaviour, IDragHandler
{
	[SerializeField] private RectTransform contentTrm;
	private Canvas canvas;

    [SerializeField] private float outBound = 10f;

	private void Awake()
	{
		canvas = GetComponentInParent<Canvas>();
    }

	public void OnDrag(PointerEventData eventData)
	{
		contentTrm.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}
}

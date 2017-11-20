using UnityEngine;
using UnityEngine.EventSystems;

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler {
	
	public Vector2 MinSize = new Vector2 (100, 100);
	public Vector2 MaxSize = new Vector2 (400, 400);
	
	private RectTransform _panelRectTransform;
	private Vector2 _originalLocalPointerPosition;
	private Vector2 _originalSizeDelta;
	
	void Awake () {
		_panelRectTransform = transform.parent.GetComponent<RectTransform> ();
	}
	
	public void OnPointerDown (PointerEventData data) {
		_originalSizeDelta = _panelRectTransform.sizeDelta;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (_panelRectTransform, data.position, data.pressEventCamera, out _originalLocalPointerPosition);
	}
	
	public void OnDrag (PointerEventData data) {
		if (_panelRectTransform == null)
			return;
		
		Vector2 localPointerPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (_panelRectTransform, data.position, data.pressEventCamera, out localPointerPosition);
		Vector3 offsetToOriginal = localPointerPosition - _originalLocalPointerPosition;
		
		Vector2 sizeDelta = _originalSizeDelta + new Vector2 (offsetToOriginal.x, -offsetToOriginal.y);
		sizeDelta = new Vector2 (
			Mathf.Clamp (sizeDelta.x, MinSize.x, MaxSize.x),
			Mathf.Clamp (sizeDelta.y, MinSize.y, MaxSize.y)
		);
		
		_panelRectTransform.sizeDelta = sizeDelta;
	}
}
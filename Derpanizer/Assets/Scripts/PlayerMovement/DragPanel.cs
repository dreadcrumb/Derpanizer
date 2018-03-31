using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler {
	
	private Vector2 _originalLocalPointerPosition;
	private Vector3 _originalPanelLocalPosition;
	private RectTransform _panelRectTransform;
	private RectTransform _parentRectTransform;
	
	void Awake () {
		_panelRectTransform = transform.parent as RectTransform;
		_parentRectTransform = _panelRectTransform.parent as RectTransform;
	}
	
	public void OnPointerDown (PointerEventData data) {
		_originalPanelLocalPosition = _panelRectTransform.localPosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle (_parentRectTransform, data.position, data.pressEventCamera, out _originalLocalPointerPosition);
	}
	
	public void OnDrag (PointerEventData data) {
		if (_panelRectTransform == null || _parentRectTransform == null)
			return;
		
		Vector2 localPointerPosition;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (_parentRectTransform, data.position, data.pressEventCamera, out localPointerPosition)) {
			Vector3 offsetToOriginal = localPointerPosition - _originalLocalPointerPosition;
			_panelRectTransform.localPosition = _originalPanelLocalPosition + offsetToOriginal;
		}
		
		ClampToWindow ();
	}
	
	// Clamp panel to area of parent
	void ClampToWindow () {
		var pos = _panelRectTransform.localPosition;
		
		Vector3 minPosition = _parentRectTransform.rect.min - _panelRectTransform.rect.min;
		Vector3 maxPosition = _parentRectTransform.rect.max - _panelRectTransform.rect.max;
		
		pos.x = Mathf.Clamp (_panelRectTransform.localPosition.x, minPosition.x, maxPosition.x);
		pos.y = Mathf.Clamp (_panelRectTransform.localPosition.y, minPosition.y, maxPosition.y);
		
		_panelRectTransform.localPosition = pos;
	}
}

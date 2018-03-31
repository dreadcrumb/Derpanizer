using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropMe : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public Image ContainerImage;
	public Image ReceivingImage;
	private Color _normalColor;
	public Color HighlightColor = Color.yellow;
	
	public void OnEnable ()
	{
		if (ContainerImage != null)
			_normalColor = ContainerImage.color;
	}
	
	public void OnDrop(PointerEventData data)
	{
		ContainerImage.color = _normalColor;
		
		if (ReceivingImage == null)
			return;
		
		var dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			ReceivingImage.overrideSprite = dropSprite;
	}

	public void OnPointerEnter(PointerEventData data)
	{
		if (ContainerImage == null)
			return;
		
		var dropSprite = GetDropSprite (data);
		if (dropSprite != null)
			ContainerImage.color = HighlightColor;
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (ContainerImage == null)
			return;
		
		ContainerImage.color = _normalColor;
	}
	
	private Sprite GetDropSprite(PointerEventData data)
	{
		var originalObj = data.pointerDrag;
		if (originalObj == null)
			return null;
		
		var dragMe = originalObj.GetComponent<DragMe>();
		if (dragMe == null)
			return null;
		
		var srcImage = originalObj.GetComponent<Image>();
		if (srcImage == null)
			return null;
		
		return srcImage.sprite;
	}
}

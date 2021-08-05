using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    RectTransform dragRectTransform;
    Canvas canvas;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        dragRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        dragRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(dragRectTransform.anchoredPosition.x, -canvas.pixelRect.width / 2, canvas.pixelRect.width / 2), Mathf.Clamp(dragRectTransform.anchoredPosition.y, (-canvas.pixelRect.height / 2) - transform.localPosition.y, (canvas.pixelRect.height / 2) - transform.localPosition.y));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragRectTransform.SetAsLastSibling();
    }
}

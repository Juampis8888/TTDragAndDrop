using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour,IBeginDragHandler,IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField]
    private Canvas Canvas;

    private CanvasGroup CanvasGroup;

    private RectTransform RectTransform;

    private void Awake()
    {   
        Canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        CanvasGroup = gameObject.GetComponent<CanvasGroup>();
        RectTransform = gameObject.GetComponent<RectTransform>();    
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CanvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform.anchoredPosition += eventData.delta/Canvas.scaleFactor;
        transform.SetParent(Canvas.transform.GetChild(0), true);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }
}

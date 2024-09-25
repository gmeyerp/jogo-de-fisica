using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class Draggable<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private T value;

    private RectTransform layoutParent;
    private Vector3 defaultPosition;
    private Dictionary<Graphic, bool> graphicDefaultRaycasts;

    [SerializeField] private bool isDraggable = true;

    protected void Start()
    {
        Graphic[] graphics = GetComponentsInChildren<Graphic>();

        graphicDefaultRaycasts = new();
        foreach (Graphic graphic in graphics)
        { graphicDefaultRaycasts.Add(graphic, graphic.raycastTarget); }

        if (GetComponentInParent<LayoutGroup>() != null)
        {
            layoutParent = GetComponentInParent<RectTransform>();
            ResetPosition = ResetPositionIfInLayoutGroup;
        }
        else
        {
            defaultPosition = transform.position;
            ResetPosition = ResetPositionIfNotInLayoutGroup;
        }
    }

    private void DisableRaycast()
    {
        foreach (Graphic image in graphicDefaultRaycasts.Keys)
        {
            image.raycastTarget = false;
        }
    }

    private void ResetRaycast()
    {
        foreach (Graphic image in graphicDefaultRaycasts.Keys)
        {
            image.raycastTarget = graphicDefaultRaycasts[image];
        }
    }

    protected void SetDraggable(bool draggable)
    {
        isDraggable = draggable;
    }

    protected UnityAction ResetPosition;
    private void ResetPositionIfInLayoutGroup()
    { LayoutRebuilder.MarkLayoutForRebuild(layoutParent); }
    private void ResetPositionIfNotInLayoutGroup()
    { transform.position = defaultPosition; }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;
        
        DisableRaycast();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!isDraggable) return;

        transform.position = eventData.position;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        ResetPosition();
        ResetRaycast();
    }

    public void SetDefaultPosition(Vector3 position)
    {
        defaultPosition = position;
    }

    public T Value => value;
    public bool CanBeDropped => isDraggable;
}

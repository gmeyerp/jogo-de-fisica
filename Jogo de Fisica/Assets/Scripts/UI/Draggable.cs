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
    private Dictionary<Image, bool> imagesDefaultRaycast;

    private void Awake()
    {
        Image[] imagesInChildren = GetComponentsInChildren<Image>();

        imagesDefaultRaycast = new();
        foreach (Image image in imagesInChildren)
        { imagesDefaultRaycast.Add(image, image.raycastTarget); }

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
        foreach (Image image in imagesDefaultRaycast.Keys)
        {
            image.raycastTarget = false;
        }
    }

    private void ResetRaycast()
    {
        foreach (Image image in imagesDefaultRaycast.Keys)
        {
            image.raycastTarget = imagesDefaultRaycast[image];
        }
    }

    private UnityAction ResetPosition;
    private void ResetPositionIfInLayoutGroup()
    { LayoutRebuilder.MarkLayoutForRebuild(layoutParent); }
    private void ResetPositionIfNotInLayoutGroup()
    { transform.position = defaultPosition; }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        DisableRaycast();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
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
}

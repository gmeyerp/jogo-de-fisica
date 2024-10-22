using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DragDropTarget<T> : MonoBehaviour, IDropHandler
{
    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent(out Draggable<T> draggable))
        {
            if (draggable.CanBeDropped)
            {
                OnDrop(draggable.Value);
            }
        }
    }

    abstract protected void OnDrop(T value);
}

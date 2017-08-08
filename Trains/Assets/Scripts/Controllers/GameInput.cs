using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : NonDrawingGraphic, IDragHandler, IPointerDownHandler, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
{
    public event System.Action DoubleTap;
    public event System.Action Tap;
    public event Action<Vector2> Drag;
    
    [SerializeField]
    private float doubleTapDuration = 0.2f;

    private bool wasDragged;
    private int dragFingerId;
    private float lastTapTime;
    private int clickCount;
    private Vector2 dragStartPosition;

    protected override void OnEnable()
    {
        base.OnEnable();

        StopAllCoroutines();
        StartCoroutine(ClickListener());
    }

    private IEnumerator ClickListener()
    {
        while (true)
        {
            if ((clickCount == 1) && (Time.time - lastTapTime > doubleTapDuration))
            {
                clickCount = 0;
                OnTap();
            }
            else if ((clickCount >= 2) && (Time.time - lastTapTime <= doubleTapDuration))
            {
                clickCount = 0;
                OnDoubleTap();
            }

            yield return null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        wasDragged = true;
        dragFingerId = eventData.pointerId;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastTapTime = Time.time;
        wasDragged = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (wasDragged && eventData.pointerId == dragFingerId)
        {
            return;
        }

        clickCount++;
    }

    private void OnTap()
    {
        if (Tap != null)
        {
            Tap();
        }
    }

    private void OnDoubleTap()
    {
        if (DoubleTap != null)
        {
            DoubleTap();
        }
    }

    private void OnDrag(Vector2 obj)
    {
        Action<Vector2> handler = Drag;
        if (handler != null) handler(obj);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        wasDragged = true;
        dragFingerId = eventData.pointerId;
        dragStartPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragFingerId == eventData.pointerId)
        {
            var delta = eventData.position - dragStartPosition;
            OnDrag(delta);
        }
    }
}

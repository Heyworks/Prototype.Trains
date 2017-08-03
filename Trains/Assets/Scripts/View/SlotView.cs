using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Image icon;
    
    [SerializeField]
    private Sprite arrowSprite;
    [SerializeField]
    private Sprite barrierSprite;
    [SerializeField]
    private Sprite ambushSprite;
    [SerializeField]
    private Image dragItem;

    public ActionObjectSlot Slot { get; private set; }
    
    private int dragId;
    private PositionConverter positionConverter;

    private void Update()
    {
        if (Slot != null)
        {
            icon.fillAmount = Slot.AvailabilityPercent;
        }
    }

    public void Setup(ActionObjectSlot slot, PositionConverter positionConverter)
    {
        Slot = slot;
        dragItem.gameObject.SetActive(false);
        this.positionConverter = positionConverter;
        SetupIcon(Slot);
    }

    private void SetupIcon(ActionObjectSlot slot)
    {
        switch (slot.ActionObjectType)
        {
            case ActionObjectType.Arrow:
                icon.sprite = arrowSprite;
                break;
            case ActionObjectType.Barrier:
                icon.sprite = barrierSprite;
                break;
            case ActionObjectType.Ambush:
                icon.sprite = ambushSprite;
                break;
            default:
                throw new ArgumentOutOfRangeException("slot");
        }

        icon.color = slot.Team == Team.Red ? Color.red : Color.blue;

        dragItem.sprite = icon.sprite;
        dragItem.color = icon.color;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragItem.transform.localPosition = Vector3.zero;
        dragItem.gameObject.SetActive(true);
        dragId = eventData.pointerId;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragId == eventData.pointerId)
        {
            dragItem.transform.position = eventData.position;
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragId == eventData.pointerId)
        {
            dragItem.gameObject.SetActive(false);
            
            if (Slot.IsActionObjectAvailable)
            {
                var viewPosition = eventData.position;
                Slot.InstallObject(positionConverter.ConvertViewToModelPosition(viewPosition));
            }
        }
    }
}
using UnityEngine;

/// <summary>
/// Represents panel view for action object.
/// </summary>
public class PanelView : MonoBehaviour
{
    [SerializeField] private SlotView[] slotViews;

    /// <summary>
    /// Initializes the specified slots.
    /// </summary>
    /// <param name="slots">The slots.</param>
    /// <param name="positionConverter">The position converter.</param>
    public void Initialize(ActionObjectSlot[] slots, PositionConverter positionConverter)
    {
        for (int i = 0; i < slotViews.Length; i++)
        {
            slotViews[i].Setup(slots[i], positionConverter);
        }
    }
}
public class SwapData
{
    public int SlotIndex { get; private set; }
    public bool IsRightSwap { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SwapData"/> class.
    /// </summary>
    /// <param name="slotIndex">Index of the slot.</param>
    /// <param name="isRightSwap">if set to <c>true</c> [is right swap].</param>
    public SwapData(int slotIndex, bool isRightSwap)
    {
        SlotIndex = slotIndex;
        IsRightSwap = isRightSwap;
    }
}
